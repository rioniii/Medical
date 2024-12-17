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
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
} from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";
import Sidebar from "./Sidebar";  // Assuming you have a Sidebar component

const Records = () => {
    const [Records, setRecords] = useState([]);
    const [openEditDialog, setOpenEditDialog] = useState(false);
    const [openAddDialog, setOpenAddDialog] = useState(false);
    const [editingService, setEditingService] = useState(null);
    const [newId, setNewId] = useState("");
    const [newServiceName, setNewServiceName] = useState("");
    const [newDescription, setNewDescription] = useState("");
    const [newPrice, setNewPrice] = useState("");

    useEffect(() => {
        fetchRecords();
    }, []);

    const fetchRecords = async () => {
        try {
            const response = await axios.get("https://localhost:7107/api/Sherbimi"); // Adjust the endpoint accordingly
            setRecords(response.data);
        } catch (error) {
            console.error("Error fetching Records:", error);
        }
    };

    const handleEdit = (service) => {
        setEditingService(service);
        setNewId(service.id); // Setting the current ID for editing
        setNewServiceName(service.emri_Sherbimit);
        setNewDescription(service.pershkrimi);
        setNewPrice(service.cmimi);
        setOpenEditDialog(true);
    };

    const handleDelete = async (serviceId) => {
        try {
            await axios.delete(`https://localhost:7107/api/Sherbimi/${serviceId}`); // Adjust the endpoint
            setRecords(Records.filter((service) => service.id !== serviceId));
        } catch (error) {
            console.error("Error deleting service:", error);
        }
    };

    const handleSaveEdit = async () => {
        try {
            const updatedService = {
                ...editingService,
                id: newId, // Ensure the ID is part of the update
                emri_Sherbimit: newServiceName,
                pershkrimi: newDescription,
                cmimi: newPrice,
            };
            await axios.put(
                `https://localhost:7107/api/Sherbimi/${editingService.id}`,
                updatedService
            );
            setRecords(Records.map((service) =>
                service.id === editingService.id ? updatedService : service
            ));
            setOpenEditDialog(false);
        } catch (error) {
            console.error("Error updating service:", error);
        }
    };

    const handleAddService = async () => {
        const newService = {
            id: newId, // Include the ID in the request
            emri_Sherbimit: newServiceName,
            pershkrimi: newDescription,
            cmimi: newPrice,
        };

        try {
            const response = await axios.post("https://localhost:7107/api/Sherbimi", newService); // Adjust the endpoint
            setRecords([...Records, response.data]);
            setOpenAddDialog(false);

            // Reset the fields after adding a service
            setNewId("");
            setNewServiceName("");
            setNewDescription("");
            setNewPrice("");
        } catch (error) {
            console.error("Error adding service:", error);
        }
    };

    return (
        <>
            <div style={{ display: 'flex', minHeight: '100vh', backgroundColor: '#f5f6f7' }}>
                <Sidebar userType="doctor" />


                <Box sx={{ padding: 4, backgroundColor: "#f4f6f8", minHeight: "100vh", width: "100%" }}>
                    <Typography variant="h4" fontWeight="bold" gutterBottom>
                        Available Records
                    </Typography>

                    <Button
                        variant="contained"
                        color="primary"
                        sx={{ marginBottom: 3 }}
                        onClick={() => setOpenAddDialog(true)}
                    >
                        Add New Service
                    </Button>

                    <Paper sx={{ overflow: "hidden", width: "100%", borderRadius: "8px", boxShadow: 3 }}>
                        <Table sx={{ width: "100%" }}>
                            <TableHead>
                                <TableRow>
                                    <TableCell><strong>Service Name</strong></TableCell>
                                    <TableCell><strong>Description</strong></TableCell>
                                    <TableCell><strong>Price</strong></TableCell>
                                    <TableCell><strong>Actions</strong></TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {Records.map((service) => (
                                    <TableRow key={service.id} sx={{ "&:nth-of-type(odd)": { backgroundColor: "#f9f9f9" } }}>
                                        <TableCell>{service.emri_Sherbimit}</TableCell>
                                        <TableCell>{service.pershkrimi}</TableCell>
                                        <TableCell>{service.cmimi.toFixed(2)}</TableCell>
                                        <TableCell>
                                            <IconButton onClick={() => handleEdit(service)} color="primary" size="small">
                                                <Edit />
                                            </IconButton>
                                            <IconButton onClick={() => handleDelete(service.id)} color="error" size="small">
                                                <Delete />
                                            </IconButton>
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </Paper>
                </Box>
            </div>

            {/* Edit Service Dialog */}
            <Dialog open={openEditDialog} onClose={() => setOpenEditDialog(false)}>
                <DialogTitle>Edit Service</DialogTitle>
                <DialogContent>
                    <TextField
                        label="Service ID"
                        fullWidth
                        value={newId}
                        onChange={(e) => setNewId(e.target.value)}
                        margin="normal"
                        disabled // Make ID field disabled for editing purposes
                    />
                    <TextField
                        label="Service Name"
                        fullWidth
                        value={newServiceName}
                        onChange={(e) => setNewServiceName(e.target.value)}
                        margin="normal"
                    />
                    <TextField
                        label="Description"
                        fullWidth
                        value={newDescription}
                        onChange={(e) => setNewDescription(e.target.value)}
                        margin="normal"
                    />
                    <TextField
                        label="Price"
                        type="number"
                        fullWidth
                        value={newPrice}
                        onChange={(e) => setNewPrice(e.target.value)}
                        margin="normal"
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenEditDialog(false)} color="secondary">
                        Cancel
                    </Button>
                    <Button onClick={handleSaveEdit} color="primary">
                        Save
                    </Button>
                </DialogActions>
            </Dialog>

            {/* Add New Service Dialog */}
            <Dialog open={openAddDialog} onClose={() => setOpenAddDialog(false)}>
                <DialogTitle>Add New Service</DialogTitle>
                <DialogContent>
                    <TextField
                        label="Service ID"
                        fullWidth
                        value={newId}
                        onChange={(e) => setNewId(e.target.value)}
                        margin="normal"
                    />
                    <TextField
                        label="Service Name"
                        fullWidth
                        value={newServiceName}
                        onChange={(e) => setNewServiceName(e.target.value)}
                        margin="normal"
                    />
                    <TextField
                        label="Description"
                        fullWidth
                        value={newDescription}
                        onChange={(e) => setNewDescription(e.target.value)}
                        margin="normal"
                    />
                    <TextField
                        label="Price"
                        type="number"
                        fullWidth
                        value={newPrice}
                        onChange={(e) => setNewPrice(e.target.value)}
                        margin="normal"
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenAddDialog(false)} color="secondary">
                        Cancel
                    </Button>
                    <Button onClick={handleAddService} color="primary">
                        Add Service
                    </Button>
                </DialogActions>
            </Dialog>
        </>
    );
};

export default Records;
