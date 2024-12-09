import React, { useState, useEffect } from "react";
import Navigation from "./Navigation.jsx";
import Button from "react-bootstrap/Button";
import Table from "react-bootstrap/Table";
import Modal from "react-bootstrap/Modal";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Container from "react-bootstrap/Container";
import { Form } from "react-bootstrap";
import axios from "axios";
import Dashboard from "./Dashboard.jsx";
import Sidebar from "./Sidebar.jsx";
import { Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTachometerAlt, faUser, faCalendar, faReceipt, faBriefcaseMedical, faCommentDots } from "@fortawesome/free-solid-svg-icons";
import { NavLink, useNavigate } from 'react-router-dom';
import Card from 'react-bootstrap/Card';
import DashboardStats from "./DashboardStats.jsx";

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

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (!token) {
            navigate("/login");
        } else {
            getData();
        }
    }, [navigate]);

    const formatDate = (dateString) => {
        return new Date(dateString).toISOString().split("T")[0];
    };

    const getData = async () => {
        const token = localStorage.getItem("token");
        try {
            const response = await axios.get("https://localhost:7107/api/Pacienti", {
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
        const payload = { Id, Name, Surname, Ditelindja };
        const token = localStorage.getItem("token");

        if (!token) {
            alert("User not authenticated.");
            return;
        }

        try {
            await axios.post("https://localhost:7107/api/Pacienti/Add-Patient", payload, {
                headers: { Authorization: `Bearer ${token}` },
            });
            getData();
            clearForm();
        } catch (error) {
            console.error("Error adding patient:", error.response?.data || error.message);
            alert("There was an error adding the patient. Please try again.");
        }
    };

    const handleEdit = (patient) => {
        setId(patient.id);
        setName(patient.name);
        setSurname(patient.surname);
        setDitelindja(formatDate(patient.ditelindja));
        handleShow();
    };

    const handleUpdatePatient = async () => {
        const updatedPatient = { Id, Name, Surname, Ditelindja };
        const token = localStorage.getItem("token");

        if (!token) {
            alert("User not authenticated.");
            return;
        }

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
                handleClose();
                getData();
            } else {
                console.error("Failed to update patient:", response.statusText);
            }
        } catch (error) {
            console.error("Error updating patient:", error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this patient?")) {
            const token = localStorage.getItem("token");

            if (!token) {
                alert("User not authenticated.");
                return;
            }

            try {
                await axios.delete(`https://localhost:7107/api/Pacienti/${id}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                alert("Patient deleted successfully!");
                getData();
            } catch (error) {
                console.error("Error deleting patient:", error);
            }
        }
    };

    return (
        <>
            <div style={{ display: 'flex', minHeight: '100vh', backgroundColor: '#f5f6f7' }}>
                <Sidebar userType="doctor" />
                <div style={{ flex: 1, padding: '20px' }}>
                    <Container>
                        <h4 className="dashboard-title">Patients</h4>
                        <DashboardStats />
                        <Row>
                            <Col xs={12} md={6}>
                                <h3>Add New Patient</h3>
                                <Form>
                                    <Form.Group className="mb-3">
                                        <Form.Label>Id</Form.Label>
                                        <Form.Control
                                            type="text"
                                            placeholder="Enter Id"
                                            value={Id}
                                            onChange={(e) => setId(e.target.value)}
                                        />
                                    </Form.Group>
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
                                    <Button variant="primary" onClick={handleAddPatient}>
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
                                                        <Button
                                                            variant="warning"
                                                            className="me-2"
                                                            onClick={() => handleEdit(item)}
                                                        >
                                                            Edit
                                                        </Button>
                                                        <Button
                                                            variant="danger"
                                                            onClick={() => handleDelete(item.id)}
                                                        >
                                                            Delete
                                                        </Button>
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
                                    <Form.Label>Id</Form.Label>
                                    <Form.Control
                                        type="text"
                                        placeholder="Enter Id"
                                        value={Id}
                                        onChange={(e) => setId(e.target.value)}
                                    />
                                </Form.Group>
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
        </>
    );
};

export default Pacientet;

