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
import { useNavigate } from 'react-router-dom';

const PatientCRUD = () => {
    const [show, setShow] = useState(false);
    const handleClose = () => {
        setShow(false);
        clearForm(); // Clear form fields on close
    };
    const handleShow = () => setShow(true);

    const [Id, setId] = useState("");
    const [Name, setName] = useState("");
    const [Surname, setSurname] = useState("");
    const [Ditelindja, setDitelindja] = useState("");
    const [data, setData] = useState([]);
    const navigate = useNavigate(); // For navigation

    useEffect(() => {
        // Check if token exists in localStorage, if not redirect to login page
        const token = localStorage.getItem('token');
        if (!token) {
            navigate('/login'); // Redirect to login page if no token
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
            // Fetch patient data from API using the token for authorization
            axios
                .get("https://localhost:7107/api/Pacienti", {
                    headers: {
                        Authorization: `Bearer ${token}`, // Use the token stored in localStorage
                        'Content-Type': 'application/json',
                    },
                })
                .then((response) => {
                    setData(response.data); // Update state with the fetched data
                })
                .catch((error) => {
                    console.error("Error fetching data:", error.response ? error.response.data : error.message);
                });

        } catch (error) {
            console.error("Error in getData:", error);
        }
    };

    const clearForm = () => {
        setId("");
        setName("");
        setSurname("");
        setDitelindja("");
    };

    const handleAddPatient = () => {
        const payload = {
            Id,
            Name,
            Surname,
            Ditelindja,
        };

        const token = localStorage.getItem("token");

        if (!token) {
            alert("User not authenticated.");
            return;
        }

        axios
            .post(
                "https://localhost:7107/api/Pacienti/Add-Patient",
                payload,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                }
            )
            .then(() => {
                getData();
                clearForm(); // Clear form fields after adding
            })
            .catch((error) => {
                console.error("Error adding patient:", error.response ? error.response.data : error.message);
                alert("There was an error adding the patient. Please try again.");
            });
    };

    const handleEdit = (patient) => {
        setId(patient.id);
        setName(patient.name);
        setSurname(patient.surname);
        setDitelindja(formatDate(patient.ditelindja)); // Format date for input
        handleShow();
    };

    const handleUpdatePatient = async () => {
        const updatedPatient = {
            Id,
            Name,
            Surname,
            Ditelindja,
        };

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
                getData(); // Refresh data after updating
            } else {
                console.error("Failed to update patient:", response.statusText);
            }
        } catch (error) {
            console.error("Error updating patient:", error);
        }
    };

    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this patient?")) {
            const token = localStorage.getItem("token");

            if (!token) {
                alert("User not authenticated.");
                return;
            }

            axios
                .delete(`https://localhost:7107/api/Pacienti/${id}`, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                })
                .then(() => {
                    alert("Patient deleted successfully!");
                    getData();
                })
                .catch((error) => {
                    console.error("Error deleting patient:", error);
                });
        }
    };

    return (
        <>
            <Navigation />
            <br />
            <div>
                <h2 className="dashboard-title">Dashboard</h2>
                <h6 className="dashboard-title" style={{ color: "grey" }}>
                    Welcome to Dashboard Doctor
                </h6>
            </div>
            <br />

            <Container className="edit-form">
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
                            <div>
                                <p></p>
                            </div>
                        </Form>
                    </Col>

                    <Col xs={12} md={6}>
                        <h3>Manage Patients</h3>
                        <Table responsive="md">
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
                                                    onClick={() => handleEdit(item)}
                                                    variant="warning"
                                                    className="me-2"
                                                >
                                                    Edit
                                                </Button>
                                                <Button onClick={() => handleDelete(item.id)} variant="danger">
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

            {/* Modal for editing patient */}
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
        </>
    );
};

export default PatientCRUD;
