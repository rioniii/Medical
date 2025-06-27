import React, { useState, useEffect } from "react";
import Button from "react-bootstrap/Button";
import Table from "react-bootstrap/Table";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Container from "react-bootstrap/Container";
import { Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import Sidebar from "./Sidebar.jsx";
import Modal from "react-bootstrap/Modal";
import "bootstrap/dist/css/bootstrap.min.css";
import { IconButton } from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";
import axios from "axios";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Room = () => {
    const [showEditModal, setShowEditModal] = useState(false);
    const [editingIndex, setEditingIndex] = useState(null);
    const [formData, setFormData] = useState({
        pacientId: "",
        pacientName: "",
        dhomaId: "",
        roomNumber: "",
        roomType: "",
        roomCapacity: "",
        isAvailable: "true",
        checkInDate: "",
        checkOutDate: "",
    });
    const [patients, setPatients] = useState([]);
    const [assignments, setAssignments] = useState([]);
    const [error, setError] = useState("");
    const navigate = useNavigate();

    const handleCloseEditModal = () => {
        setShowEditModal(false);
        setEditingIndex(null);
        setFormData({
            pacientId: "",
            pacientName: "",
            dhomaId: "",
            roomNumber: "",
            roomType: "",
            roomCapacity: "",
            isAvailable: "true",
            checkInDate: "",
            checkOutDate: "",
        });
        setError("");
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const fetchData = async () => {
        const token = localStorage.getItem("token");
        if (!token) {
            navigate("/login");
        } else {
            try {
                const patientsResponse = await fetch("https://localhost:7107/api/Pacienti", {
                    method: "GET",
                    headers: {
                        "Authorization": `Bearer ${token}`,
                        "Content-Type": "application/json",
                    }
                });

                if (patientsResponse.ok) {
                    const patientsData = await patientsResponse.json();
                    setPatients(patientsData);
                } else {
                    const errorMsg = "Data could not be fetched.";
                    setError(errorMsg);
                    toast.error(errorMsg);
                    throw new Error(errorMsg);
                }
            } catch (error) {
                const errorMsg = `Error fetching data: ${error.message}`;
                console.error(errorMsg);
                setError(errorMsg);
                toast.error(errorMsg);
            }
        }
    };

    const fetchAssignments = async () => {
        const token = localStorage.getItem("token");
        try {
            const response = await fetch("https://localhost:7107/api/DhomaPacientit", {
                headers: { "Authorization": `Bearer ${token}` }
            });
            if (response.ok) {
                const data = await response.json();
                setAssignments(data);
            } else {
                const errorMsg = `Failed to fetch assignments: ${response.status}`;
                setError(errorMsg);
                toast.error(errorMsg);
            }
        } catch (err) {
            const errorMsg = `Failed to fetch assignments: ${err.message}`;
            setError(errorMsg);
            toast.error(errorMsg);
        }
    };

    useEffect(() => {
        fetchData();
        fetchAssignments();
    }, []);

    const generateGuid = () => {
        return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
            var r = (Math.random() * 16) | 0,
                v = c === "x" ? r : (r & 0x3) | 0x8;
            return v.toString(16);
        });
    };

    const handleSubmitAdd = async (e) => {
        e.preventDefault();

        if (!formData.pacientId || !formData.roomNumber || !formData.roomType || !formData.roomCapacity || !formData.checkInDate) {
            setError("Patient, Room, and Check-in Date are required.");
            return;
        }

        if (formData.checkOutDate && new Date(formData.checkOutDate) < new Date(formData.checkInDate)) {
            setError("Check-out date cannot be before the check-in date.");
            return;
        }

        const token = localStorage.getItem("token");
        const newAssignment = {
            pacientId: formData.pacientId,
            dhomaId: formData.dhomaId,
            checkInDate: formData.checkInDate,
            checkOutDate: formData.checkOutDate,
            roomNumber: formData.roomNumber,
            roomType: formData.roomType,
            roomCapacity: formData.roomCapacity,
            isAvailable: formData.isAvailable === "true",
        };

        try {
            const response = await axios.post("https://localhost:7107/api/DhomaPacientit", newAssignment, {
                headers: {
                    "Authorization": `Bearer ${token}`,
                    "Content-Type": "application/json",
                },
            });
            if (response.ok) {
                fetchAssignments();
                handleCloseEditModal();
                toast.success('Room assignment added successfully!');
            } else {
                setError("Failed to add assignment.");
            }
        } catch (error) {
            const errorMsg = error.response?.data?.title || "Failed to add room assignment.";
            console.error("Error adding room assignment:", error);
            setError(errorMsg);
            toast.error(errorMsg);
        }
    };

    const handleSubmitEdit = (e) => {
        e.preventDefault();

        if (!formData.pacientName || !formData.roomNumber || !formData.roomType || !formData.roomCapacity || !formData.checkInDate) {
            setError("Patient Name, Room, and Check-in Date are required.");
            return;
        }

        if (formData.checkOutDate && new Date(formData.checkOutDate) < new Date(formData.checkInDate)) {
            setError("Check-out date cannot be before the check-in date.");
            return;
        }

        const updatedAssignment = {
            pacientId: formData.pacientId,
            pacientName: formData.pacientName,
            dhomaId: formData.dhomaId,
            checkInDate: formData.checkInDate,
            checkOutDate: formData.checkOutDate || "N/A",
            roomNumber: formData.roomNumber,
            roomType: formData.roomType,
            roomCapacity: formData.roomCapacity,
            isAvailable: formData.isAvailable === "true",
        };

        setAssignments((prevAssignments) => {
            const updatedAssignments = [...prevAssignments];
            updatedAssignments[editingIndex] = updatedAssignment;
            return updatedAssignments;
        });

        alert("Room assignment updated successfully!");
        handleCloseEditModal();
    };

    const handleEdit = (index) => {
        setEditingIndex(index);
        const selectedAssignment = assignments[index];

        setFormData({
            pacientId: selectedAssignment.pacientId,
            pacientName: selectedAssignment.pacientName,
            dhomaId: selectedAssignment.dhomaId,
            roomNumber: selectedAssignment.roomNumber,
            roomType: selectedAssignment.roomType,
            roomCapacity: selectedAssignment.roomCapacity,
            isAvailable: selectedAssignment.isAvailable ? "true" : "false",
            checkInDate: selectedAssignment.checkInDate,
            checkOutDate: selectedAssignment.checkOutDate !== "N/A" ? selectedAssignment.checkOutDate : "",
        });

        setShowEditModal(true);
    };

    const handleDelete = (index) => {
        if (window.confirm("Are you sure you want to delete this assignment?")) {
            setAssignments(assignments.filter((_, i) => i !== index));
        }
    };

    const availablePatients = patients.filter(
        (patient) => !assignments.some((assignment) => assignment.pacientId === patient.id)
    );

    return (
        <div style={{ display: "flex", minHeight: "100vh", backgroundColor: "#f5f6f7" }}>
            <Sidebar userType="doctor" />
            <div style={{ flex: 1, padding: "20px" }}>
                <Container>
                    <h3 className="dashboard-title">Room Assignments</h3>
                    <Row>
                        <Col xs={12} md={6}>
                            <h4>Add New Room Assignment</h4>
                            <Form onSubmit={handleSubmitAdd}>
                                <Form.Group className="mb-3">
                                    <Form.Label>Patient</Form.Label>
                                    <Form.Control
                                        as="select"
                                        name="pacientId"
                                        value={formData.pacientId}
                                        onChange={handleChange}
                                        required
                                    >
                                        <option value="">Select Patient</option>
                                        {availablePatients.map((patient) => (
                                            <option key={patient.id} value={patient.id}>
                                                {patient.name} {patient.surname}
                                            </option>
                                        ))}
                                    </Form.Control>
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Room Number</Form.Label>
                                    <Form.Control
                                        type="text"
                                        name="roomNumber"
                                        value={formData.roomNumber}
                                        onChange={handleChange}
                                        required
                                    />
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Room Type</Form.Label>
                                    <Form.Control
                                        type="text"
                                        name="roomType"
                                        value={formData.roomType}
                                        onChange={handleChange}
                                        required
                                    />
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Room Capacity</Form.Label>
                                    <Form.Control
                                        type="number"
                                        name="roomCapacity"
                                        value={formData.roomCapacity}
                                        onChange={handleChange}
                                        required
                                    />
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Is Available?</Form.Label>
                                    <Form.Control
                                        as="select"
                                        name="isAvailable"
                                        value={formData.isAvailable}
                                        onChange={handleChange}
                                        required
                                    >
                                        <option value="true">Yes</option>
                                        <option value="false">No</option>
                                    </Form.Control>
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Check-in Date</Form.Label>
                                    <Form.Control
                                        type="date"
                                        name="checkInDate"
                                        value={formData.checkInDate}
                                        onChange={handleChange}
                                        required
                                    />
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Check-out Date (Optional)</Form.Label>
                                    <Form.Control
                                        type="date"
                                        name="checkOutDate"
                                        value={formData.checkOutDate}
                                        onChange={handleChange}
                                    />
                                </Form.Group>
                                <Button variant="primary" type="submit">
                                    Add Assignment
                                </Button>
                            </Form>
                            {error && <div className="alert alert-danger mt-3">{error}</div>}
                        </Col>
                        <Col xs={12} md={6}>
                            <h4>Current Room Assignments</h4>
                            <Table striped bordered hover responsive="sm" size="sm">
                                <thead>
                                    <tr>
                                        <th>Patient ID</th>
                                        <th>Patient</th>
                                        <th>Room</th>
                                        <th>Availability</th>
                                        <th>Check-in Date</th>
                                        <th>Check-out Date</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {assignments.length > 0 ? (
                                        assignments.map((assignment, index) => (
                                            <tr key={index}>
                                                <td>{assignment.pacientId}</td>
                                                <td>{assignment.pacientName}</td>
                                                <td>{assignment.roomNumber}</td>
                                                <td style={{ color: assignment.isAvailable ? "green" : "red", fontWeight: "bold" }}>
                                                    {assignment.isAvailable ? "Available" : "Not Available"}
                                                </td>
                                                <td>{assignment.checkInDate}</td>
                                                <td>{assignment.checkOutDate}</td>
                                                <td>
                                                    <IconButton
                                                        color="primary"
                                                        onClick={() => handleEdit(index)}
                                                        sx={{ mr: 1 }}
                                                    >
                                                        <Edit />
                                                    </IconButton>
                                                    <IconButton
                                                        color="error"
                                                        onClick={() => handleDelete(index)}
                                                    >
                                                        <Delete />
                                                    </IconButton>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="7" className="text-center">No room assignments available.</td>
                                        </tr>
                                    )}
                                </tbody>
                            </Table>
                        </Col>
                    </Row>
                </Container>
            </div>

            <Modal show={showEditModal} onHide={handleCloseEditModal} centered>
                <Modal.Header closeButton>
                    <Modal.Title>Edit Room Assignment</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form onSubmit={handleSubmitEdit}>
                        <Form.Group className="mb-3">
                            <Form.Label>Patient Name</Form.Label>
                            <Form.Control
                                type="text"
                                name="pacientName"
                                value={formData.pacientName}
                                onChange={handleChange}
                                required
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Room Number</Form.Label>
                            <Form.Control
                                type="text"
                                name="roomNumber"
                                value={formData.roomNumber}
                                onChange={handleChange}
                                required
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Room Type</Form.Label>
                            <Form.Control
                                type="text"
                                name="roomType"
                                value={formData.roomType}
                                onChange={handleChange}
                                required
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Room Capacity</Form.Label>
                            <Form.Control
                                type="number"
                                name="roomCapacity"
                                value={formData.roomCapacity}
                                onChange={handleChange}
                                required
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Is Available?</Form.Label>
                            <Form.Control
                                as="select"
                                name="isAvailable"
                                value={formData.isAvailable}
                                onChange={handleChange}
                                required
                            >
                                <option value="true">Yes</option>
                                <option value="false">No</option>
                            </Form.Control>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Check-in Date</Form.Label>
                            <Form.Control
                                type="date"
                                name="checkInDate"
                                value={formData.checkInDate}
                                onChange={handleChange}
                                required
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Check-out Date (Optional)</Form.Label>
                            <Form.Control
                                type="date"
                                name="checkOutDate"
                                value={formData.checkOutDate}
                                onChange={handleChange}
                            />
                        </Form.Group>
                        <Button variant="primary" type="submit">
                            Update Assignment
                        </Button>
                    </Form>
                    {error && <div className="alert alert-danger mt-3">{error}</div>}
                </Modal.Body>
            </Modal>
        </div>
    );
};

export default Room;