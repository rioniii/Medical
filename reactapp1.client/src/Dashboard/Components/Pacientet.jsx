import React, { useState, useEffect } from "react";
import Button from "react-bootstrap/Button";
import Table from "react-bootstrap/Table";
import Modal from "react-bootstrap/Modal";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Container from "react-bootstrap/Container";
import { Form } from "react-bootstrap";
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import { v4 as uuidv4 } from 'uuid';
import Sidebar from "./Sidebar.jsx";
import { IconButton } from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";

const Pacientet = () => {
    const [show, setShow] = useState(false);
    const [Id, setId] = useState("");
    const [Name, setName] = useState("");
    const [Surname, setSurname] = useState("");
    const [Ditelindja, setDitelindja] = useState("");
    const [data, setData] = useState([]);
    const navigate = useNavigate();

    const handleClose = () => {
        setShow(false);
        clearForm();
    };

    const handleShow = () => setShow(true);

    const decodeJWT = (token) => {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const decoded = atob(base64);
        return JSON.parse(decoded);
    };

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (!token) {
            navigate("/login");
        } else {
            try {
                const decoded = decodeJWT(token);
                console.log(decoded);
                getData(decoded.id);
            } catch (error) {
                console.error("Invalid token", error);
                navigate("/login");
            }
        }
    }, [navigate]);

    const formatDate = (dateString) => {
        return new Date(dateString).toISOString().split("T")[0];
    };

    const getData = async (userId) => {
        const token = localStorage.getItem("token");
        try {
            const response = await axios.get(`https://localhost:7107/api/Pacienti?userId=${userId}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "application/json",
                },
            });
            setData(response.data);
        } catch (error) {
            console.error("Error fetching data:", error.response?.data || error.message);
        }
    };

    const clearForm = () => {
        setId("");
        setName("");
        setSurname("");
        setDitelindja("");
    };

    const handleAddPatient = async () => {
        const token = localStorage.getItem("token");

        if (!token) {
            alert("User not authenticated.");
            return;
        }

        const decoded = decodeJWT(token);
        const userId = decoded.id || decoded.uid;

        const today = new Date();
        const dob = new Date(Ditelindja);
        if (dob > today) {
            alert("Date of Birth cannot be in the future.");
            return;
        }

        const newPatientId = uuidv4();

        const payload = { Id: newPatientId, Name, Surname, Ditelindja };
        console.log("Payload to send:", payload);

        if (!Name || !Surname || !Ditelindja) {
            alert("Please fill out all required fields.");
            return;
        }

        try {
            const response = await axios.post("https://localhost:7107/api/Pacienti/Add-Patient", payload, {
                headers: { Authorization: `Bearer ${token}` },
            });

            const existingPatients = JSON.parse(localStorage.getItem('patients')) || [];
            existingPatients.push({ id: newPatientId, name: Name, surname: Surname, ditelindja: Ditelindja });
            localStorage.setItem('patients', JSON.stringify(existingPatients));
            
            getData(userId);
            clearForm();
        } catch (error) {
            if (error.response) {
                console.error("Error adding patient:", error.response.data);
                alert(`Error adding patient: ${error.response?.data?.message || 'Unknown error'}`);
            } else {
                console.error("Error adding patient:", error.message);
                alert("There was an error adding the patient. Please try again.");
            }
        }
    };

    const handleEdit = (patient) => {
        console.log("Editing patient:", patient);
        setId(patient.id);
        setName(patient.name);
        setSurname(patient.surname);
        setDitelindja(formatDate(patient.ditelindja));
        handleShow();
    };

    const handleUpdatePatient = async () => {
        const token = localStorage.getItem("token");

        if (!token) {
            alert("User not authenticated.");
            return;
        }

        const decoded = decodeJWT(token);

        const today = new Date();
        const dob = new Date(Ditelindja);
        if (dob > today) {
            alert("Date of Birth cannot be in the future.");
            return;
        }

        const updatedPatient = { Id, Name, Surname, Ditelindja };
        console.log("Updating patient with payload:", updatedPatient);

        try {
            const response = await fetch(
                `https://localhost:7107/api/Pacienti/Update-Pacienti?id=${Id}`,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`,
                    },
                    body: JSON.stringify(updatedPatient),
                }
            );

            if (response.ok) {
                console.log("Patient updated successfully");
                handleClose();
                getData(decoded.id);
            } else {
                console.error("Failed to update patient:", response.statusText);
                alert("Error updating patient.");
            }
        } catch (error) {
            console.error("Error updating patient:", error);
            alert("There was an error updating the patient. Please try again.");
        }
    };

    const handleDelete = async (id) => {
        console.log("Deleting patient with ID:", id);
        if (window.confirm("Are you sure you want to delete this patient?")) {
            const token = localStorage.getItem("token");

            if (!token) {
                alert("User not authenticated.");
                return;
            }

            const decoded = decodeJWT(token);

            try {
                const response = await axios.delete(`https://localhost:7107/api/Pacienti/${id}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });

                if (response.status === 200) {
                    console.log("Patient deleted successfully");
                    alert("Patient deleted successfully!");
                    getData(decoded.id);
                } else {
                    console.error("Failed to delete patient:", response.statusText);
                    alert("Error deleting patient.");
                }
            } catch (error) {
                console.error("Error deleting patient:", error);
                alert("There was an error deleting the patient. Please try again.");
            }
        }
    };

    return (
        <div style={{ display: 'flex', minHeight: '100vh', backgroundColor: '#f5f6f7' }}>
            <Sidebar userType="doctor" />
            <div style={{ flex: 1, padding: '20px' }}>
                <Container>
                    <h4 className="dashboard-title">Patients</h4>
                    <Row>
                        <Col xs={12} md={6}>
                            <h3>Add New Patient</h3>
                            <Form>
                                <Form.Group className="mb-3">
                                    <Form.Label>Emri</Form.Label>
                                    <Form.Control
                                        type="text"
                                        placeholder="Enter Emri"
                                        value={Name}
                                        onChange={(e) => setName(e.target.value)}
                                    />
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Mbiemri</Form.Label>
                                    <Form.Control
                                        type="text"
                                        placeholder="Enter Mbiemri"
                                        value={Surname}
                                        onChange={(e) => setSurname(e.target.value)}
                                    />
                                </Form.Group>
                                <Form.Group className="mb-3">
                                    <Form.Label>Viti i Lindjes</Form.Label>
                                    <Form.Control
                                        type="date"
                                        placeholder="Enter Viti Lindjes"
                                        value={Ditelindja}
                                        onChange={(e) => setDitelindja(e.target.value)}
                                    />
                                </Form.Group>
                                <Button variant="success" onClick={handleAddPatient}>
                                    Add Patient
                                </Button>
                            </Form>
                        </Col>
                        <Col xs={12} md={6}>
                            <h3>Patient List</h3>
                            <Table striped bordered hover responsive="sm" size="sm">
                                <thead>
                                    <tr>
                                        <th>Id</th>
                                        <th>Emri</th>
                                        <th>Mbiemri</th>
                                        <th>Ditelindja</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {data.length > 0 ? (
                                        data.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.id}</td>
                                                <td>{item.name}</td>
                                                <td>{item.surname}</td>
                                                <td>{formatDate(item.ditelindja)}</td>
                                                <td>
                                                    <IconButton 
                                                        color="primary" 
                                                        onClick={() => handleEdit(item)}
                                                        sx={{ mr: 1 }}
                                                    >
                                                        <Edit />
                                                    </IconButton>
                                                    <IconButton 
                                                        color="error" 
                                                        onClick={() => handleDelete(item.id)}
                                                    >
                                                        <Delete />
                                                    </IconButton>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="5" style={{ textAlign: "center" }}>
                                                No data available or failed to load data.
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </Table>
                        </Col>
                    </Row>
                </Container>

                <Modal show={show} onHide={handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title>Edit Patient</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group className="mb-3">
                                <Form.Label>Emri</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter Emri"
                                    value={Name}
                                    onChange={(e) => setName(e.target.value)}
                                />
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Mbiemri</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter Mbiemri"
                                    value={Surname}
                                    onChange={(e) => setSurname(e.target.value)}
                                />
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Viti i Lindjes</Form.Label>
                                <Form.Control
                                    type="date"
                                    placeholder="Enter Viti Lindjes"
                                    value={Ditelindja}
                                    onChange={(e) => setDitelindja(e.target.value)}
                                />
                            </Form.Group>
                            <Button variant="primary" onClick={handleUpdatePatient}>
                                Update Patient
                            </Button>
                        </Form>
                    </Modal.Body>
                </Modal>
            </div>
        </div>
    );
};

export default Pacientet;