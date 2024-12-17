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

const Records = () => {
    const [historiku, setHistoriku] = useState([]);
    const [newEntry, setNewEntry] = useState({
        Id: "",
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

    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState("");

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
        if (!newEntry.MjekuId || !newEntry.PacientId || !newEntry.TerminiId || !newEntry.Data) {
            setMessage("All fields are required.");
            return;
        }

        setLoading(true);
        try {
            const formattedDate = formatDate(newEntry.Data);  // Use formatDate to format the date

            const newData = {
                Id: newEntry.Id,
                mjekuId: newEntry.MjekuId,
                pacientId: newEntry.PacientId,
                terminiId: newEntry.TerminiId,
                data: formattedDate,  // Use formatted date
                anamneza_Statusi: newEntry.Anamneza_Statusi || "",
                ekzaminimi: newEntry.Ekzaminimi || "",
                diagnoza: newEntry.Diagnoza || "",
                terapia: newEntry.Terapia || "",
                perfundimi: newEntry.Perfundimi || "",
            };

            console.log("Request Data:", newData); // Log the request data

            const response = await axios.post("https://localhost:7107/api/Historiku", newData);
            setMessage("Entry added successfully!");
            fetchHistoriku();
            setNewEntry({}); // Clear form after successful submission
        } catch (error) {
            console.error("Error adding entry:", error.response?.data || error.message);
            setMessage("Error adding entry. Please check the fields and try again.");
        } finally {
            setLoading(false);
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

    const handleEdit = (id) => {
        const entryToEdit = historiku.find((entry) => entry.id === id);
        setNewEntry(entryToEdit); // Populate the form with the data to edit
    };

    // Handle updating an existing entry
    const handleUpdate = async () => {
        if (!newEntry.MjekuId || !newEntry.PacientId || !newEntry.TerminiId || !newEntry.Data) {
            setMessage("All fields are required.");
            return;
        }

        setLoading(true);
        try {
            const formattedDate = formatDate(newEntry.Data); // Use formatDate to format the date

            const updatedData = {
                Id: newEntry.Id,
                mjekuId: newEntry.MjekuId,
                pacientId: newEntry.PacientId,
                terminiId: newEntry.TerminiId,
                data: formattedDate, // Use formatted date
                anamneza_Statusi: newEntry.Anamneza_Statusi || "",
                ekzaminimi: newEntry.Ekzaminimi || "",
                diagnoza: newEntry.Diagnoza || "",
                terapia: newEntry.Terapia || "",
                perfundimi: newEntry.Perfundimi || "",
            };

            console.log("Updated Request Data:", updatedData); // Log the request data

            const response = await axios.put(`https://localhost:7107/api/Historiku/${newEntry.Id}`, updatedData);
            setMessage("Entry updated successfully!");
            fetchHistoriku();
            setNewEntry({}); // Clear form after successful submission
        } catch (error) {
            console.error("Error updating entry:", error.response?.data || error.message);
            setMessage("Error updating entry. Please check the fields and try again.");
        } finally {
            setLoading(false);
        }
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
                    <Grid item xs={12} sm={6}>
                        <TextField
                            label="ID"
                            name="Id"
                            value={newEntry.PacientId || ""}
                            onChange={handleChange}
                            fullWidth
                        />
                    </Grid>
                    <br></br>
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
                    </Grid><br />
                    <Button
                        variant="contained"
                        color="success"
                        onClick={handleAdd}
                        disabled={loading}
                    >
                        {loading ? "Adding..." : "Add Entry"}
                    </Button>
                    {message && <Typography color="error">{message}</Typography>}
                </Paper>

                {/* History Table */}
                <Paper sx={{ overflow: "hidden" }}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Id</TableCell>
                                <TableCell>Mjeku ID</TableCell>
                                <TableCell>Pacient ID</TableCell>
                                <TableCell>Termini ID</TableCell>
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
                                    <TableCell>{entry.Id}</TableCell>
                                    <TableCell>{entry.mjekuId}</TableCell>
                                    <TableCell>{entry.pacientId}</TableCell>
                                    <TableCell>{entry.terminiId}</TableCell>
                                    <TableCell>{entry.data}</TableCell>
                                    <TableCell>{entry.anamneza_Statusi}</TableCell>
                                    <TableCell>{entry.ekzaminimi}</TableCell>
                                    <TableCell>{entry.diagnoza}</TableCell>
                                    <TableCell>{entry.terapia}</TableCell>
                                    <TableCell>{entry.perfundimi}</TableCell>
                                    <TableCell>
                                        <IconButton style={{ color: 'blue' }} onClick={() => handleEdit(entry.id)} > 
                                            <Edit />
                                        </IconButton>
                                        <IconButton style={{ color:'red' }} onClick={() => handleDelete(entry.id)}>
                                            <Delete  />
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

export default Records;
