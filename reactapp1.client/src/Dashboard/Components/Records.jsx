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
    Paper,
    Button,
    IconButton,
    TextField,
    Grid,
} from "@mui/material";
import { FaConciergeBell } from "react-icons/fa"; // Concierge Bell icon
import { Delete, Edit } from "@mui/icons-material";
import Sidebar from "./Sidebar"; // Sidebar included


const Services = () => {
    const [historiku, setHistoriku] = useState([]);
    const [newEntry, setNewEntry] = useState({
        MjekuId: "",
        PacientId: "",
        TerminiId: "", // Added TerminiId field
        Data: "",
        Anamneza_Statusi: "",
        Ekzaminimi: "",
        Diagnoza: "",
        Terapia: "",
        Perfundimi: "",
    });

    // Fetch the historical data
    useEffect(() => {
        fetchHistoriku();
    }, []);

    const fetchHistoriku = async () => {
        try {
            const response = await axios.get("https://localhost:7107/api/Historiku"); // Adjust the endpoint accordingly
            setHistoriku(response.data);
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };

    // Handle form input changes
    const handleChange = (e) => {
        setNewEntry({
            ...newEntry,
            [e.target.name]: e.target.value,
        });
    };

    const formatDate = (dateString) => {
        const date = new Date(dateString);
        if (isNaN(date.getTime())) {
            throw new Error('Invalid date format');
        }
        return date.toISOString().split("T")[0];
    };

    // handleAdd function
    const handleAdd = async () => {
        try {
            const formattedDate = formatDate(newEntry.data);  // Use formatDate to format the date

            const newData = {
                id: newEntry.id || "",
                mjekuId: newEntry.mjekuId || "",
                pacientId: newEntry.pacientId || "",
                data: formattedDate,  // Use formatted date
                anamneza_Statusi: newEntry.anamneza_Statusi || "",
                ekzaminimi: newEntry.ekzaminimi || "",
                diagnoza: newEntry.diagnoza || "",
                terapia: newEntry.terapia || "",
                perfundimi: newEntry.perfundimi || "",
            };

            const response = await axios.post("https://localhost:7107/api/Historiku", newData);

            fetchHistoriku();
            setNewEntry({});  // Clear form after successful submission
        } catch (error) {
            console.error("Error adding entry:", error);
            if (error.response && error.response.data) {
                console.error("Validation errors: ", error.response.data.errors);
            }
        }
    };






    // Handle deleting an entry
    const handleDelete = async (id) => {
        try {
            await axios.delete(`https://localhost:7107/api/Historiku/${id}`); // Adjust API endpoint
            fetchHistoriku(); // Refresh the list after deleting
        } catch (error) {
            console.error("Error deleting entry:", error);
        }
    };

    // Handle editing an entry
    const handleEdit = (id) => {
        const entryToEdit = historiku.find((entry) => entry.id === id);
        setNewEntry(entryToEdit); // Populate the form with the data to edit
    };

    return (
        <Box sx={{ display: "flex", minHeight: "100vh", backgroundColor: "#f4f6f8" }}>

            <Sidebar userType="doctor" />


            <Box sx={{ flex: 1, padding: 4 }}>
                <Typography variant="h4" fontWeight="bold" gutterBottom>
                    Patient History 
                </Typography>

                {/* Add New Entry Form */}
                <Paper sx={{ padding: 3, marginBottom: 4 }}>
                    <Typography variant="h6" gutterBottom>
                        Add New Entry
                    </Typography>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Mjeku ID"
                                name="MjekuId"
                                value={newEntry.MjekuId || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Pacient ID"
                                name="PacientId"
                                value={newEntry.PacientId || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Termini ID"
                                name="TerminiId" // TerminiId field
                                value={newEntry.TerminiId || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Date"
                                type="date"
                                name="Data"
                                value={newEntry.Data || ""}
                                onChange={handleChange}
                                fullWidth
                                InputLabelProps={{ shrink: true }}
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Anamneza Status"
                                name="Anamneza_Statusi"
                                value={newEntry.Anamneza_Statusi || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Examination"
                                name="Ekzaminimi"
                                value={newEntry.Ekzaminimi || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Diagnosis"
                                name="Diagnoza"
                                value={newEntry.Diagnoza || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Therapy"
                                name="Terapia"
                                value={newEntry.Terapia || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                label="Conclusion"
                                name="Perfundimi"
                                value={newEntry.Perfundimi || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                    </Grid><br></br>
                    <Button variant="contained" color="primary" onClick={handleAdd}>
                        Add Entry
                    </Button>
                </Paper>

                {/* History Table */}
                <Paper sx={{ overflow: "hidden" }}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Mjeku ID</TableCell>
                                <TableCell>Pacient ID</TableCell>
                                <TableCell>Termini ID</TableCell> {/* Display Termini ID */}
                                <TableCell>Date</TableCell>
                                <TableCell>Anamneza Status</TableCell>
                                <TableCell>Examination</TableCell>
                                <TableCell>Diagnosis</TableCell>
                                <TableCell>Therapy</TableCell>
                                <TableCell>Conclusion</TableCell>
                                <TableCell>Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {historiku.map((entry) => (
                                <TableRow key={entry.id}>
                                    <TableCell>{entry.mjekuId}</TableCell>
                                    <TableCell>{entry.pacientId}</TableCell>
                                    <TableCell>{entry.terminiId}</TableCell> {/* Display Termini ID */}
                                    <TableCell>{new Date(entry.data).toLocaleDateString()}</TableCell>
                                    <TableCell>{entry.anamneza_Statusi}</TableCell>
                                    <TableCell>{entry.ekzaminimi}</TableCell>
                                    <TableCell>{entry.diagnoza}</TableCell>
                                    <TableCell>{entry.terapia}</TableCell>
                                    <TableCell>{entry.perfundimi}</TableCell>
                                    <TableCell>
                                        <IconButton onClick={() => handleEdit(entry.id)} color="primary">
                                            <Edit />
                                        </IconButton>
                                        <IconButton onClick={() => handleDelete(entry.id)} color="error">
                                            <Delete />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </Paper>
            </Box>
        </Box>
    );
};

export default Services;
