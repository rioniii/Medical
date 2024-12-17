import React, { useEffect, useState } from "react";
import axios from "axios";
import { Calendar, momentLocalizer } from "react-big-calendar";
import moment from "moment";
import "react-big-calendar/lib/css/react-big-calendar.css";
import {
    Box,
    Typography,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
    IconButton,
    MenuItem,
    Select,
    InputLabel,
    FormControl
} from "@mui/material";
import { Delete, Edit, Add } from "@mui/icons-material";
import Sidebar from "./Sidebar";

const localizer = momentLocalizer(moment);

const Appointments = () => {
    const doctorId = "1D";  // Hardcoding doctorId as "1D"
    const [appointments, setAppointments] = useState([]);
    const [selectedAppointment, setSelectedAppointment] = useState(null);
    const [modalOpen, setModalOpen] = useState(false);
    const [doctor, setDoctor] = useState(null);
    const [newAppointment, setNewAppointment] = useState({
        pacientId: "",
        name: "",
        surname: "",
        dataTerminit: moment().format("YYYY-MM-DDTHH:mm:ss"),
        statusi: "",
    });

    // Fetch appointments for doctor with id "1D"
    const fetchAppointments = async () => {
        try {
            const response = await axios.get(
                `https://localhost:7107/api/Termini/ShowAppointments-OfDoctor?doctorId=${doctorId}`
            );
            const formattedAppointments = response.data.map((appointment) => ({
                title: `${appointment.name} ${appointment.surname}`,
                start: new Date(appointment.dataTerminit),
                end: new Date(appointment.dataTerminit),
                id: appointment.id,
                pacientId: appointment.pacientId,
                status: appointment.statusi,
                doktorId: appointment.doktorId,
            }));
            setAppointments(formattedAppointments);
        } catch (error) {
            console.error("Failed to fetch appointments:", error);
        }
    };

    // Fetch doctor info (doctor with id "1D")
    const fetchDoctorInfo = async () => {
        try {
            const response = await axios.get(
                `https://localhost:7107/api/Doctors/${doctorId}`
            );
            setDoctor(response.data);  // Assuming the response contains doctor info
        } catch (error) {
            console.error("Failed to fetch doctor information:", error);
        }
    };

    useEffect(() => {
        fetchAppointments();
        fetchDoctorInfo();
    }, []);

    const handleEventClick = (appointment) => {
        setSelectedAppointment(appointment);
        setNewAppointment({
            pacientId: appointment.pacientId,
            name: appointment.title.split(" ")[0],  // First name
            surname: appointment.title.split(" ")[1] || "",  // Last name
            dataTerminit: moment(appointment.start).format("YYYY-MM-DDTHH:mm:ss"),
            statusi: appointment.status,
        });
        setModalOpen(true);
    };

    const handleCloseModal = () => {
        setModalOpen(false);
        setSelectedAppointment(null);
        setDoctor(null);  // Reset doctor info when modal is closed
        setNewAppointment({
            pacientId: "",
            name: "",
            surname: "",
            dataTerminit: moment().format("YYYY-MM-DDTHH:mm:ss"),
            statusi: "",
        });  // Reset new appointment form when modal is closed
    };

    const handleUpdateAppointment = async () => {
        try {
            const updatedData = {
                id: selectedAppointment.id,
                pacientId: newAppointment.pacientId,
                dataTerminit: moment(newAppointment.dataTerminit).format("YYYY-MM-DDTHH:mm:ss"),
                statusi: newAppointment.statusi,
                name: newAppointment.name,
                surname: newAppointment.surname,
                doktorId: doctorId,
            };

            await axios.put(`https://localhost:7107/api/Termini/${selectedAppointment.id}`, updatedData);
            alert("Appointment updated successfully!");
            fetchAppointments();  // Refresh the appointments after update
            handleCloseModal();  // Close the modal
        } catch (error) {
            console.error("Failed to update appointment:", error.response?.data || error.message);
            alert(`Failed to update appointment: ${JSON.stringify(error.response?.data.errors || error.message)}`);
        }
    };

    const handleDeleteAppointment = async () => {
        try {
            await axios.delete(
                `https://localhost:7107/api/Termini/${selectedAppointment.id}`
            );
            fetchAppointments();
            handleCloseModal();
        } catch (error) {
            console.error("Failed to delete appointment:", error);
        }
    };

    const handleAddAppointment = async () => {
        try {
            const newAppointmentData = {
                id: new Date().getTime().toString(),  // Generate a unique Id using timestamp (or use any unique identifier)
                doktorId: doctorId, // Pre-set Doctor ID from the URL
                pacientId: newAppointment.pacientId,
                dataTerminit: moment(newAppointment.dataTerminit).format("YYYY-MM-DDTHH:mm:ss"),
                statusi: newAppointment.statusi,
                name: newAppointment.name,
                surname: newAppointment.surname,
            };

            await axios.post("https://localhost:7107/api/Termini", newAppointmentData);
            alert("Appointment added successfully!");
            fetchAppointments();  // Refresh the appointments after addition
            handleCloseModal();  // Close the modal
        } catch (error) {
            console.error("Failed to add appointment:", error.response?.data || error.message);
            alert(`Failed to add appointment: ${JSON.stringify(error.response?.data.errors || error.message)}`);
        }
    };

    return (
        <Box sx={{ display: "flex", minHeight: "100vh", backgroundColor: "#f5f6f7" }}>
            <Sidebar userType="doctor" />
            <Box sx={{ flex: 1, padding: 4 }}>
                <Typography variant="h4" gutterBottom>
                    Appointments Calendar
                </Typography>
                <Calendar
                    localizer={localizer}
                    events={appointments}
                    startAccessor="start"
                    endAccessor="end"
                    style={{ height: 600 }}
                    views={["month", "week", "day"]}
                    defaultView="month"
                    popup
                    onSelectEvent={handleEventClick}
                />

                {/* Modal for Appointment Details */}
                <Dialog open={modalOpen} onClose={handleCloseModal}>
                    <DialogTitle>{selectedAppointment ? "Appointment Details" : "Add Appointment"}</DialogTitle>
                    <DialogContent>
                        <TextField
                            label="Patient Name"
                            value={newAppointment.name}
                            onChange={(e) =>
                                setNewAppointment({ ...newAppointment, name: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                            disabled={selectedAppointment ? true : false}
                        />
                        <TextField
                            label="Patient Surname"
                            value={newAppointment.surname}
                            onChange={(e) =>
                                setNewAppointment({ ...newAppointment, surname: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                            disabled={selectedAppointment ? true : false}
                        />
                        <TextField
                            label="Patient ID"
                            value={newAppointment.pacientId}
                            onChange={(e) =>
                                setNewAppointment({ ...newAppointment, pacientId: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Appointment Date"
                            type="datetime-local"
                            value={newAppointment.dataTerminit}
                            onChange={(e) =>
                                setNewAppointment({ ...newAppointment, dataTerminit: e.target.value })
                            }
                            fullWidth
                            margin="normal"
                        />

                        {/* Doctor ID Field */}
                        <TextField
                            label="Doctor ID"
                            value={doctorId}  // Display fixed doctor ID "1D"
                            fullWidth
                            margin="normal"
                            disabled
                        />

                        {/* Status Dropdown */}
                        <FormControl fullWidth margin="normal">
                            <InputLabel>Status</InputLabel>
                            <Select
                                value={newAppointment.statusi}
                                onChange={(e) =>
                                    setNewAppointment({ ...newAppointment, statusi: e.target.value })
                                }
                                label="Status"
                            >
                                <MenuItem value="">Status..</MenuItem>
                                <MenuItem value="Pending">Pending</MenuItem>
                                <MenuItem value="Scheduled">Scheduled</MenuItem>
                                <MenuItem value="Cancelled">Cancelled</MenuItem>
                                <MenuItem value="Finished">Finished</MenuItem>

                            </Select>
                        </FormControl>
                    </DialogContent>
                    <DialogActions>
                        {selectedAppointment ? (
                            <>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    startIcon={<Edit />}
                                    onClick={handleUpdateAppointment}
                                >
                                    Update
                                </Button>
                                <Button
                                    variant="contained"
                                    color="error"
                                    startIcon={<Delete />}
                                    onClick={handleDeleteAppointment}
                                >
                                    Delete
                                </Button>
                            </>
                        ) : (
                            <Button
                                variant="contained"
                                color="primary"
                                startIcon={<Add />}
                                onClick={handleAddAppointment}
                            >
                                Add Appointment
                            </Button>
                        )}
                        <Button onClick={handleCloseModal}>Cancel</Button>
                    </DialogActions>
                </Dialog>

                {/* Floating Action Button for Adding Appointment */}
                <IconButton
                    sx={{
                        position: "fixed",
                        bottom: 16,
                        right: 16,
                        backgroundColor: "#4caf50",
                        color: "white",
                    }}
                    onClick={() => setModalOpen(true)}
                >
                    <Add />
                </IconButton>
            </Box>
        </Box>
    );
};

export default Appointments;
