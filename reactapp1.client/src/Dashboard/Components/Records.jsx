import React, { useEffect, useState } from "react";
import axios from "axios";
import { v4 as uuidv4 } from "uuid";
import {
    Box,
    Typography,
    Paper,
    Button,
    TextField,
    Grid,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
} from "@mui/material";
import Sidebar from "./Sidebar";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Records = () => {
    const [newEntry, setNewEntry] = useState({
        Id: "",
        MjekuId: "",
        PacientId: "",
        Data: "",
        Anamneza_Statusi: "",
        Ekzaminimi: "",
        Diagnoza: "",
        Terapia: "",
        Perfundimi: "",
    });

    const [patients, setPatients] = useState([]);
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState("");
    const [error, setError] = useState("");

    useEffect(() => {
        fetchPatients();
        fetchLoggedInDoctorId();
    }, []);

    const fetchPatients = async () => {
        try {
            const token = localStorage.getItem("token");
            if (!token) {
                console.error("Unauthorized: No token found.");
                return;
            }
            const config = { headers: { Authorization: `Bearer ${token}` } };
            const response = await axios.get("https://localhost:7107/api/Pacienti", config);
            if (Array.isArray(response.data)) {
                setPatients(response.data);
            } else {
                const errorMsg = "Invalid patient data format.";
            setError(errorMsg);
            toast.error(errorMsg);
            }
        } catch (error) {
            const errorMsg = `Error fetching patients: ${error.message}`;
            setError(errorMsg);
            console.error("Error fetching patients:", error);
            toast.error(errorMsg);
        }
    };

    const fetchLoggedInDoctorId = () => {
        const token = localStorage.getItem("token");
        if (token) {
            try {
                const decodedToken = JSON.parse(atob(token.split(".")[1]));
                const doctorId = decodedToken.uid;
                if (doctorId) {
                    setNewEntry((prevEntry) => ({
                        ...prevEntry,
                        MjekuId: doctorId,
                    }));
                }
            } catch (error) {
                const errorMsg = error.response?.data?.title || "Error decoding token. Please try again.";
                console.error("Error decoding token:", error);
                setError(errorMsg);
                toast.error(errorMsg);
            }
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setNewEntry({
            ...newEntry,
            [name]: value,
        });
    };

    const handlePatientSelect = (e) => {
        const selectedPatientId = e.target.value;
        setNewEntry({
            ...newEntry,
            PacientId: selectedPatientId,
        });
    };

    const validateEntry = () => {
        if (!newEntry.MjekuId || !newEntry.PacientId || !newEntry.Data) {
            const errorMsg = "All fields are required.";
            setError(errorMsg);
            toast.error(errorMsg);
            return false;
        }

        const inputDate = new Date(newEntry.Data);
        const today = new Date();

        // Normalize both to midnight
        inputDate.setHours(0, 0, 0, 0);
        today.setHours(0, 0, 0, 0);

        if (isNaN(inputDate)) {
            setError("Invalid date format.");
            return false;
        }

        if (inputDate.getTime() !== today.getTime()) {
            setError("Date must be today.");
            return false;
        }

        return true;
    };

    const handleAdd = async () => {
        if (!validateEntry()) return;
        setLoading(true);
        try {
            const newData = {
                Id: uuidv4(),
                MjekuId: newEntry.MjekuId,
                PacientId: newEntry.PacientId,
                Data: new Date(newEntry.Data).toISOString(),
                Anamneza_Statusi: newEntry.Anamneza_Statusi || "",
                Ekzaminimi: newEntry.Ekzaminimi || "",
                Diagnoza: newEntry.Diagnoza || "",
                Terapia: newEntry.Terapia || "",
                Perfundimi: newEntry.Perfundimi || "",
            };
            await axios.post("https://localhost:7107/api/Historiku", newData);
            const successMsg = "Record saved successfully!";
            setMessage(successMsg);
            setError("");
            toast.success(successMsg);
            setNewEntry({
                Id: "",
                MjekuId: newEntry.MjekuId,
                PacientId: "",
                Data: "",
                Anamneza_Statusi: "",
                Ekzaminimi: "",
                Diagnoza: "",
                Terapia: "",
                Perfundimi: "",
            });
        } catch (error) {
            setError("Error adding entry: " + error.message);
            setMessage("");
        } finally {
            setLoading(false);
        }
    };

    const todayStr = new Date().toISOString().split("T")[0];

    return (
        <Box sx={{ display: "flex", minHeight: "100vh", backgroundColor: "#f4f6f8" }}>
            <Sidebar userType="doctor" />
            <Box sx={{ flex: 1, padding: 4 }}>
                <Typography variant="h4" fontWeight="bold" gutterBottom>
                    Add Patient History
                </Typography>
                <Paper sx={{ padding: 3 }}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} sm={6}>
                            <FormControl fullWidth>
                                <InputLabel>Select Patient</InputLabel>
                                <Select
                                    name="PacientId"
                                    value={newEntry.PacientId || ""}
                                    onChange={handlePatientSelect}
                                    label="Select Patient"
                                >
                                    {patients.map((patient) => (
                                        <MenuItem key={patient.id} value={patient.id}>
                                            {`${patient.name} ${patient.surname}`}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
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
                                inputProps={{
                                    min: todayStr,
                                    max: todayStr,
                                }}
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
                        <Grid item xs={12}>
                            <TextField
                                label="Conclusion"
                                name="Perfundimi"
                                value={newEntry.Perfundimi || ""}
                                onChange={handleChange}
                                fullWidth
                            />
                        </Grid>
                    </Grid>
                    <Button
                        variant="contained"
                        color="success"
                        onClick={handleAdd}
                        disabled={loading}
                        sx={{ mt: 2 }}
                    >
                        {loading ? "Adding..." : "Add Entry"}
                    </Button>
                    {message && <Typography color="success.main" sx={{ mt: 2 }}>{message}</Typography>}
                    {error && <Typography color="error.main" sx={{ mt: 2 }}>{error}</Typography>}
                </Paper>
            </Box>
        </Box>
    );
};

export default Records;
