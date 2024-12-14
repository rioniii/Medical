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
    Checkbox,
    FormControlLabel,
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

    const [fatureForm, setFatureForm] = useState({
        id: "",
        pacientId: "",
        sherbimiId: "",
        shuma: "",
        data: moment().format("YYYY-MM-DD"),
        paguar: false,
    });

    const [isEditMode, setIsEditMode] = useState(false);

    useEffect(() => {
        fetchPayments();
    }, []);

    const fetchPayments = async () => {
        try {
            const response = await axios.get("https://localhost:7107/api/Fatura");
            setPayments(response.data);
            setFilteredPayments(response.data);
        } catch (error) {
            console.error("Error fetching payments:", error);
        }
    };

    const handleFilter = () => {
        const filtered = payments.filter((payment) => {
            const matchesSearchTerm = searchTerm
                ? payment.pacientId.toLowerCase().includes(searchTerm.toLowerCase())
                : true;
            const matchesStatus = statusFilter ? payment.paguar === (statusFilter === "Paid") : true;
            const matchesDateRange = dateRangeFilter
                ? moment(payment.data).isSame(dateRangeFilter, "day")
                : true;

            return matchesSearchTerm && matchesStatus && matchesDateRange;
        });
        setFilteredPayments(filtered);
    };

    const handleAddFature = async () => {
        try {
            await axios.post("https://localhost:7107/api/Fatura", fatureForm);
            fetchPayments();
            setOpenAddDialog(false);
            resetForm();
        } catch (error) {
            console.error("Error adding Fature:", error);
        }
    };

    const handleEditFature = async () => {
        try {
            await axios.put(`https://localhost:7107/api/Fatura/${fatureForm.id}`, fatureForm);
            fetchPayments();
            setOpenEditDialog(false);
            resetForm();
        } catch (error) {
            console.error("Error updating Fature:", error);
        }
    };

    const handleDeleteFature = async (id) => {
        try {
            await axios.delete(`https://localhost:7107/api/Fatura/${id}`);
            fetchPayments();
        } catch (error) {
            console.error("Error deleting Fature:", error);
        }
    };

    const openEditDialogHandler = (payment) => {
        setFatureForm(payment);
        setIsEditMode(true);
        setOpenEditDialog(true);
    };

    const resetForm = () => {
        setFatureForm({
            id: "",
            pacientId: "",
            sherbimiId: "",
            shuma: "",
            data: moment().format("YYYY-MM-DD"),
            paguar: false,
        });
    };

    return (
        <Box sx={{ display: "flex", minHeight: "100vh", backgroundColor: "#f0f9f4" }}>
            <Sidebar userType="doctor" />
            <Box sx={{ flex: 1, padding: 4 }}>
                <Typography variant="h4" fontWeight="bold" gutterBottom>
                    Payments
                </Typography>

                {/* Add Fature Button */}
                <Box sx={{ display: "flex", justifyContent: "flex-start", mb: 3 }}>
                    <Button
                        variant="contained"
                        color="success"
                        startIcon={<Add />}
                        onClick={() => {
                            resetForm();
                            setOpenAddDialog(true);
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
                        Add Fature
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
                        <Grid item xs={12} sm={6} md={4}>
                            <TextField
                                label="Search Patients"
                                value={searchTerm}
                                onChange={(e) => setSearchTerm(e.target.value)}
                                fullWidth
                                variant="outlined"
                            />
                        </Grid>
                        <Grid item xs={12} sm={6} md={3}>
                            <FormControl fullWidth>
                                <InputLabel>Status</InputLabel>
                                <Select
                                    value={statusFilter}
                                    onChange={(e) => setStatusFilter(e.target.value)}
                                    variant="outlined"
                                >
                                    <MenuItem value="">All Status</MenuItem>
                                    <MenuItem value="Paid">Paid</MenuItem>
                                    <MenuItem value="Pending">Pending</MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>
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
                <Paper elevation={3} sx={{ borderRadius: 3, overflow: "hidden" }}>
                    <Table>
                        <TableHead>
                            <TableRow
                                sx={{
                                    backgroundColor: "#2e7d32",
                                    "& th": { color: "#ffffff", fontWeight: "bold" },
                                }}
                            >
                                <TableCell>Fature ID</TableCell>
                                <TableCell>Patient</TableCell>
                                <TableCell>Service</TableCell>
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
                                    <TableCell>{payment.pacientId}</TableCell>
                                    <TableCell>{payment.sherbimiId}</TableCell>
                                    <TableCell>
                                        {moment(payment.data).format("MMM DD, YYYY")}
                                    </TableCell>
                                    <TableCell>
                                        {payment.paguar ? (
                                            <Typography color="green">Paid</Typography>
                                        ) : (
                                            <Typography color="orange">Pending</Typography>
                                        )}
                                    </TableCell>
                                    <TableCell>${payment.shuma.toFixed(2)}</TableCell>
                                    <TableCell>
                                        <IconButton
                                            color="success"
                                            onClick={() => openEditDialogHandler(payment)}
                                        >
                                            <Edit />
                                        </IconButton>
                                        <IconButton
                                            color="error"
                                            onClick={() => handleDeleteFature(payment.id)}
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
                    <DialogTitle>
                        {isEditMode ? "Edit Fature" : "Add New Fature"}
                    </DialogTitle>
                    <DialogContent>
                        <TextField
                            label="Fature ID"
                            value={fatureForm.id}
                            onChange={(e) =>
                                setFatureForm({ ...fatureForm, id: e.target.value })
                            }
                            fullWidth
                            disabled={isEditMode}
                            margin="normal"
                        />
                        <TextField
                            label="Patient ID"
                            value={fatureForm.pacientId}
                            onChange={(e) =>
                                setFatureForm({ ...fatureForm, pacientId: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Service ID"
                            value={fatureForm.sherbimiId}
                            onChange={(e) =>
                                setFatureForm({ ...fatureForm, sherbimiId: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            type="number"
                            label="Amount"
                            value={fatureForm.shuma}
                            onChange={(e) =>
                                setFatureForm({ ...fatureForm, shuma: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            type="date"
                            label="Date"
                            value={fatureForm.data}
                            onChange={(e) =>
                                setFatureForm({ ...fatureForm, data: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                        />
                        <FormControlLabel
                            control={
                                <Checkbox
                                    checked={fatureForm.paguar}
                                    onChange={(e) =>
                                        setFatureForm({ ...fatureForm, paguar: e.target.checked })
                                    }
                                />
                            }
                            label="Paid"
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button
                            onClick={() => {
                                setOpenAddDialog(false);
                                setOpenEditDialog(false);
                            }}
                            color="secondary"
                        >
                            Cancel
                        </Button>
                        <Button
                            onClick={isEditMode ? handleEditFature : handleAddFature}
                            color="success"
                        >
                            {isEditMode ? "Update" : "Add"}
                        </Button>
                    </DialogActions>
                </Dialog>
            </Box>
        </Box>
    );
};

export default Payments;
