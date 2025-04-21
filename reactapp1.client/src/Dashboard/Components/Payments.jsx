import React, { useEffect, useState } from "react";
import axios from "axios";
import {
    Box,
    Typography,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    TextField,
    Button,
    MenuItem,
    Select,
    InputLabel,
    FormControl,
    IconButton,
    Grid,
    Paper,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from "@mui/material";
import { FilterList, Add, Edit, Delete } from "@mui/icons-material";
import moment from "moment";
import Sidebar from "./Sidebar";

const Payments = () => {
    const [payments, setPayments] = useState([]);
    const [filteredPayments, setFilteredPayments] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [statusFilter, setStatusFilter] = useState("");
    const [dateRangeFilter, setDateRangeFilter] = useState("");
    const [openAddDialog, setOpenAddDialog] = useState(false);
    const [openEditDialog, setOpenEditDialog] = useState(false);
    const [paymentForm, setPaymentForm] = useState({
        id: "",
        patientId: "",
        patientName: "",
        amount: "",
        date: moment().format("YYYY-MM-DD"),
        status: "Progress",
    });
    const [isEditMode, setIsEditMode] = useState(false);
    const [patients, setPatients] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    // Get the localStorage key for payments based on the token
    const getPaymentsKey = () => {
        const token = localStorage.getItem("token");
        return token ? `payments_${token}` : null;
    };

    // Load payments from localStorage on component mount
    useEffect(() => {
        const paymentsKey = getPaymentsKey();
        if (paymentsKey) {
            const savedPayments = localStorage.getItem(paymentsKey);
            if (savedPayments) {
                try {
                    const parsedPayments = JSON.parse(savedPayments);
                    setPayments(parsedPayments);
                    setFilteredPayments(parsedPayments);
                } catch (error) {
                    console.error("Error parsing payments from localStorage:", error);
                }
            }
        }
        fetchPatients();
    }, []);

    // Save payments to localStorage whenever payments change
    useEffect(() => {
        const paymentsKey = getPaymentsKey();
        if (paymentsKey) {
            localStorage.setItem(paymentsKey, JSON.stringify(payments));
        }
    }, [payments]);

    // Fetch patients from the API
    const fetchPatients = async () => {
        try {
            const token = localStorage.getItem("token");
            if (!token) {
                console.error("Unauthorized: No token found.");
                return;
            }
            const config = { headers: { Authorization: `Bearer ${token}` } };
            const response = await axios.get("https://localhost:7107/api/User", config);
            setPatients(response.data);
        } catch (error) {
            setError("Error fetching patients: " + error.message);
            console.error("Error fetching patients:", error);
        } finally {
            setLoading(false);
        }
    };

    // Handle adding a new payment
    const handleAddPayment = () => {
        if (!paymentForm.patientId || !paymentForm.amount || !paymentForm.date) {
            alert("Please fill in all fields.");
            return;
        }

        // Check if the patient already has a payment
        const patientAlreadyHasPayment = payments.some(
            (payment) => payment.patientId === paymentForm.patientId
        );

        if (patientAlreadyHasPayment) {
            alert("This patient already has a payment. Please edit the existing payment instead.");
            return;
        }

        // Date validation
        const selectedDate = moment(paymentForm.date);
        const today = moment().startOf("day");
        if (selectedDate.isBefore(today)) {
            alert("Date cannot be earlier than today.");
            return;
        }

        // Create a new payment object
        const newPayment = {
            id: Date.now().toString(),
            patientId: paymentForm.patientId,
            patientName: getPatientNameById(paymentForm.patientId),
            amount: parseFloat(paymentForm.amount),
            date: paymentForm.date,
            status: paymentForm.status,
        };

        // Update payments state
        const updatedPayments = [newPayment, ...payments];
        setPayments(updatedPayments);
        setFilteredPayments(updatedPayments);

        // Close the dialog and reset the form
        setOpenAddDialog(false);
        resetForm();
    };

    // Handle editing an existing payment
    const handleEditPayment = () => {
        if (!paymentForm.patientName || !paymentForm.amount || !paymentForm.date) {
            alert("Please fill in all fields.");
            return;
        }

        // Ensure amount is treated as a number
        const amount = parseFloat(paymentForm.amount);
        if (isNaN(amount)) {
            alert("Amount must be a valid number.");
            return;
        }

        // Date validation
        const selectedDate = moment(paymentForm.date);
        const today = moment().startOf("day");
        if (selectedDate.isBefore(today)) {
            alert("Date cannot be earlier than today.");
            return;
        }

        // Update the payment in the list
        const updatedPayments = payments.map((payment) =>
            payment.id === paymentForm.id
                ? { ...paymentForm, amount: amount } // Ensure amount is a number
                : payment
        );

        setPayments(updatedPayments);
        setFilteredPayments(updatedPayments);

        // Close the dialog and reset the form
        setOpenEditDialog(false);
        resetForm();
    };

    // Handle deleting a payment
    const handleDeletePayment = (id) => {
        const updatedPayments = payments.filter((payment) => payment.id !== id);
        setPayments(updatedPayments);
        setFilteredPayments(updatedPayments);
    };

    // Reset the payment form
    const resetForm = () => {
        setPaymentForm({
            id: "",
            patientId: "",
            patientName: "",
            amount: "",
            date: moment().format("YYYY-MM-DD"),
            status: "Progress",
        });
    };

    // Filter payments based on search term, status, and date range
    const handleFilter = () => {
        const filtered = payments.filter((payment) => {
            const matchesSearchTerm = searchTerm
                ? payment.patientId === searchTerm // Filter by patient ID
                : true;
            const matchesStatus = statusFilter ? payment.status === statusFilter : true;
            const matchesDateRange = dateRangeFilter
                ? moment(payment.date).isSame(dateRangeFilter, "day")
                : true;

            return matchesSearchTerm && matchesStatus && matchesDateRange;
        });
        setFilteredPayments(filtered);
    };

    // Get patient name by ID
    const getPatientNameById = (id) => {
        const patient = patients.find((patient) => patient.id === id);
        return patient ? `${patient.firstName} ${patient.lastName}` : "Unknown";
    };

    // Clear payments when token is removed or expires
    const clearPayments = () => {
        const paymentsKey = getPaymentsKey();
        if (paymentsKey) {
            localStorage.removeItem(paymentsKey);
        }
        setPayments([]);
        setFilteredPayments([]);
    };

    // Check token status periodically
    useEffect(() => {
        const checkToken = () => {
            const token = localStorage.getItem("token");
            if (!token) {
                clearPayments();
            }
        };

        const interval = setInterval(checkToken, 5000);
        return () => clearInterval(interval);
    }, []);

    if (loading) {
        return <div>Loading...</div>;
    }

    return (
        <Box sx={{ display: "flex", minHeight: "100vh", backgroundColor: "#f0f9f4" }}>
            <Sidebar userType="doctor" />
            <Box sx={{ flex: 1, padding: 4 }}>
                <Typography variant="h4" fontWeight="bold" gutterBottom>
                    Payments
                </Typography>

                {/* Add Payment Button */}
                <Box sx={{ display: "flex", justifyContent: "flex-start", mb: 3 }}>
                    <Button
                        variant="contained"
                        color="success"
                        startIcon={<Add />}
                        onClick={() => {
                            resetForm();
                            setOpenAddDialog(true);
                            setIsEditMode(false);
                        }}
                        sx={{
                            textTransform: "none",
                            fontWeight: "bold",
                            borderRadius: 2,
                            backgroundColor: "#2e7d32",
                            "&:hover": {
                                backgroundColor: "#1b5e20",
                            },
                        }}
                    >
                        Add Payment
                    </Button>
                </Box>

                {/* Filters */}
                <Paper
                    elevation={3}
                    sx={{
                        padding: 3,
                        mb: 4,
                        borderRadius: 2,
                        backgroundColor: "#ffffff",
                    }}
                >
                    <Grid container spacing={2} alignItems="center">
                        {/* Patient Filter */}
                        <Grid item xs={12} sm={6} md={4}>
                            <FormControl fullWidth>
                                <InputLabel>Patient</InputLabel>
                                <Select
                                    value={searchTerm}
                                    onChange={(e) => setSearchTerm(e.target.value)}
                                    variant="outlined"
                                >
                                    <MenuItem value="">All Patients</MenuItem>
                                    {patients.map((patient) => (
                                        <MenuItem key={patient.id} value={patient.id}>
                                            {`${patient.firstName} ${patient.lastName}`}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        </Grid>

                        {/* Status Filter */}
                        <Grid item xs={12} sm={6} md={3}>
                            <FormControl fullWidth>
                                <InputLabel>Status</InputLabel>
                                <Select
                                    value={statusFilter}
                                    onChange={(e) => setStatusFilter(e.target.value)}
                                    variant="outlined"
                                >
                                    <MenuItem value="">All Statuses</MenuItem>
                                    <MenuItem value="Paid">Paid</MenuItem>
                                    <MenuItem value="Progress">Progress</MenuItem>
                                    <MenuItem value="Canceled">Canceled</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>

                        {/* Date Filter */}
                        <Grid item xs={12} sm={6} md={3}>
                            <TextField
                                type="date"
                                label="Date"
                                InputLabelProps={{ shrink: true }}
                                value={dateRangeFilter}
                                onChange={(e) => setDateRangeFilter(e.target.value)}
                                fullWidth
                                variant="outlined"
                            />
                        </Grid>

                        {/* Filter Button */}
                        <Grid item xs={12} sm={6} md={2}>
                            <Button
                                variant="contained"
                                onClick={handleFilter}
                                startIcon={<FilterList />}
                                fullWidth
                                sx={{
                                    textTransform: "none",
                                    backgroundColor: "#2e7d32",
                                    "&:hover": {
                                        backgroundColor: "#1b5e20",
                                    },
                                }}
                            >
                                Filter
                            </Button>
                        </Grid>
                    </Grid>
                </Paper>

                {/* Payments Table */}
                <Paper elevation={0} sx={{ borderRadius: 3, overflow: "hidden" }}>
                    <Table sx={{ minWidth: 650 }}>
                        <TableHead>
                            <TableRow
                                sx={{
                                    backgroundColor: "#2e7d32",
                                    "& th": { color: "#ffffff", fontWeight: "bold" },
                                }}
                            >
                                <TableCell>Payment ID</TableCell>
                                <TableCell>Patient</TableCell>
                                <TableCell>Date</TableCell>
                                <TableCell>Status</TableCell>
                                <TableCell>Amount</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {filteredPayments.map((payment) => (
                                <TableRow
                                    key={payment.id}
                                    sx={{
                                        "&:hover": {
                                            backgroundColor: "#e8f5e9",
                                        },
                                    }}
                                >
                                    <TableCell>{payment.id}</TableCell>
                                    <TableCell>{payment.patientName}</TableCell>
                                    <TableCell>{moment(payment.date).format("MMM DD, YYYY")}</TableCell>
                                    <TableCell>
                                        {payment.status === "Paid" ? (
                                            <Typography color="green">Paid</Typography>
                                        ) : payment.status === "Progress" ? (
                                            <Typography color="orange">Progress</Typography>
                                        ) : (
                                            <Typography color="red">Canceled</Typography>
                                        )}
                                    </TableCell>
                                    <TableCell>${payment.amount.toFixed(2)}</TableCell>
                                    <TableCell>
                                        <IconButton
                                            onClick={() => {
                                                setPaymentForm(payment);
                                                setIsEditMode(true);
                                                setOpenEditDialog(true);
                                            }}
                                        >
                                            <Edit />
                                        </IconButton>
                                        <IconButton
                                            color="error"
                                            onClick={() => handleDeletePayment(payment.id)}
                                        >
                                            <Delete />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </Paper>

                {/* Add/Edit Dialog */}
                <Dialog
                    open={openAddDialog || openEditDialog}
                    onClose={() => {
                        setOpenAddDialog(false);
                        setOpenEditDialog(false);
                    }}
                >
                    <DialogTitle>{isEditMode ? "Edit Payment" : "Add Payment"}</DialogTitle>
                    <DialogContent>
                        {/* Patient Field */}
                        {isEditMode ? (
                            <TextField
                                label="Patient"
                                value={paymentForm.patientName}
                                onChange={(e) =>
                                    setPaymentForm({ ...paymentForm, patientName: e.target.value })
                                }
                                fullWidth
                                margin="normal"
                            />
                        ) : (
                            <FormControl fullWidth margin="normal">
                                <InputLabel>Patient</InputLabel>
                                <Select
                                    value={paymentForm.patientId}
                                    onChange={(e) =>
                                        setPaymentForm({ ...paymentForm, patientId: e.target.value })
                                    }
                                    fullWidth
                                    variant="outlined"
                                >
                                    <MenuItem value="">Select Patient</MenuItem>
                                    {patients.map((patient) => (
                                        <MenuItem key={patient.id} value={patient.id}>
                                            {`${patient.firstName} ${patient.lastName}`}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        )}

                        {/* Amount Field */}
                        <TextField
                            label="Amount"
                            type="number"
                            value={paymentForm.amount}
                            onChange={(e) => setPaymentForm({ ...paymentForm, amount: e.target.value })}
                            fullWidth
                            margin="normal"
                        />

                        {/* Date Field */}
                        <TextField
                            label="Date"
                            type="date"
                            value={paymentForm.date}
                            onChange={(e) => setPaymentForm({ ...paymentForm, date: e.target.value })}
                            fullWidth
                            InputLabelProps={{
                                shrink: true,
                            }}
                            margin="normal"
                        />

                        {/* Status Field */}
                        <FormControl fullWidth margin="normal">
                            <InputLabel>Status</InputLabel>
                            <Select
                                value={paymentForm.status}
                                onChange={(e) =>
                                    setPaymentForm({
                                        ...paymentForm,
                                        status: e.target.value,
                                    })
                                }
                                variant="outlined"
                            >
                                <MenuItem value="Paid">Paid</MenuItem>
                                <MenuItem value="Progress">Progress</MenuItem>
                                <MenuItem value="Canceled">Canceled</MenuItem>
                            </Select>
                        </FormControl>
                    </DialogContent>
                    <DialogActions>
                        <Button
                            onClick={() => {
                                setOpenAddDialog(false);
                                setOpenEditDialog(false);
                            }}
                            color="primary"
                        >
                            Cancel
                        </Button>
                        <Button
                            onClick={isEditMode ? handleEditPayment : handleAddPayment}
                            color="primary"
                        >
                            {isEditMode ? "Save Changes" : "Add Payment"}
                        </Button>
                    </DialogActions>
                </Dialog>
            </Box>
        </Box>
    );
};

export default Payments;