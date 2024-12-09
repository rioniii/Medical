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
} from "@mui/material";
import { FilterList, MoreVert, CalendarToday, Payment, Timeline } from "@mui/icons-material";
import moment from "moment";
import Sidebar from "./Sidebar"; // Sidebar included

const Payments = () => {
    const [payments, setPayments] = useState([]);
    const [filteredPayments, setFilteredPayments] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [statusFilter, setStatusFilter] = useState("");
    const [dateRangeFilter, setDateRangeFilter] = useState("");

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

    const formatAmount = (amount) => {
        return amount.toFixed(2);
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

    return (
        <Box sx={{ display: "flex", minHeight: "100vh", backgroundColor: "#f4f6f8" }}>
            <Sidebar userType="doctor" />
            <Box sx={{ flex: 1, padding: 4 }}>
                <Typography variant="h4" fontWeight="bold" gutterBottom>
                    Payments
                </Typography>

                {/* Stats Section */}
                <Grid container spacing={2} mb={4}>
                    <Grid item xs={12} sm={4}>
                        <Paper
                            elevation={3}
                            sx={{
                                padding: 3,
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                            }}
                        >
                            <Box>
                                <Typography variant="subtitle1" color="textSecondary">
                                    Today Payments
                                </Typography>
                                <Typography variant="h4" fontWeight="bold">
                                    442,236
                                </Typography>
                                <Typography variant="body2" color="textSecondary">
                                    Transactions today
                                </Typography>
                            </Box>
                            <CalendarToday fontSize="large" color="primary" />
                        </Paper>
                    </Grid>

                    <Grid item xs={12} sm={4}>
                        <Paper
                            elevation={3}
                            sx={{
                                padding: 3,
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                            }}
                        >
                            <Box>
                                <Typography variant="subtitle1" color="textSecondary">
                                    Monthly Payments
                                </Typography>
                                <Typography variant="h4" fontWeight="bold">
                                    1,242,500
                                </Typography>
                                <Typography variant="body2" color="textSecondary">
                                    Transactions this month
                                </Typography>
                            </Box>
                            <Payment fontSize="large" color="secondary" />
                        </Paper>
                    </Grid>

                    <Grid item xs={12} sm={4}>
                        <Paper
                            elevation={3}
                            sx={{
                                padding: 3,
                                display: "flex",
                                alignItems: "center",
                                justifyContent: "space-between",
                            }}
                        >
                            <Box>
                                <Typography variant="subtitle1" color="textSecondary">
                                    Yearly Payments
                                </Typography>
                                <Typography variant="h4" fontWeight="bold">
                                    34,570,000
                                </Typography>
                                <Typography variant="body2" color="textSecondary">
                                    Transactions this year
                                </Typography>
                            </Box>
                            <Timeline fontSize="large" color="success" />
                        </Paper>
                    </Grid>
                </Grid>

                {/* Filters */}
                <Box sx={{ display: "flex", gap: 2, mb: 2 }}>
                    <TextField
                        label="Search 'Patients'"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                        fullWidth
                    />
                    <FormControl fullWidth>
                        <InputLabel>Status</InputLabel>
                        <Select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
                            <MenuItem value="">All Status</MenuItem>
                            <MenuItem value="Paid">Paid</MenuItem>
                            <MenuItem value="Pending">Pending</MenuItem>
                            <MenuItem value="Cancelled">Cancelled</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        type="date"
                        label="Date"
                        InputLabelProps={{ shrink: true }}
                        value={dateRangeFilter}
                        onChange={(e) => setDateRangeFilter(e.target.value)}
                        fullWidth
                    />
                    <Button variant="contained" onClick={handleFilter} startIcon={<FilterList />}>
                        Filter
                    </Button>
                </Box>

                {/* Payments Table */}
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Patient</TableCell>
                            <TableCell>Date</TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell>Amount</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {filteredPayments.map((payment) => (
                            <TableRow key={payment.id}>
                                <TableCell>{payment.pacientId}</TableCell>
                                <TableCell>{moment(payment.data).format("MMM DD, YYYY")}</TableCell>
                                <TableCell>
                                    {payment.paguar ? (
                                        <Typography color="green">Paid</Typography>
                                    ) : (
                                        <Typography color="orange">Pending</Typography>
                                    )}
                                </TableCell>
                                <TableCell>{formatAmount(payment.shuma)}</TableCell>
                                <TableCell>
                                    <IconButton>
                                        <MoreVert />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </Box>
        </Box>
    );
};

export default Payments;
