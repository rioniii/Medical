import React, { useState, useEffect } from "react";
import Navigation from "./Navigation.jsx";
import Button from "react-bootstrap/Button";
import Table from 'react-bootstrap/Table';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import axios from 'axios';
import { Form } from 'react-bootstrap';

const PatientCRUD = () => {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [data, setData] = useState([]);
    const [Emri, setEmri] = useState('');
    const [Mbiemri, setMbiemri] = useState('');
    const [Gjinia, setGjinia] = useState('M');
    const [VitiLindjes, setVitiLindjes] = useState('');
    const [Historiku, setHistoriku] = useState('');
    const [Ditelindja, setDitelindja] = useState('');

    useEffect(() => {
        getData();
    }, []);

    const getData = () => {
        axios.get('https://localhost:7107/api/Pacienti')
            .then(response => {
                setData(response.data);
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    };

    const handleAddPatient = () => {
        const payload = {
            Historiku,
            Ditelindja,
        };

        axios.post('https://localhost:7107/api/Pacienti', payload)
            .then(() => {
                getData();
                setHistoriku('');
                setDitelindja('');
            })
            .catch(error => console.error('Error adding patient:', error));
    };

    const handleEdit = (patient) => {
        setHistoriku(patient.historiku);
        setDitelindja(patient.ditelindja);
        handleShow();
    };

    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this patient?")) {
            axios.delete(`https://localhost:7107/api/Pacienti/${id}`)
                .then(() => {
                    alert("Patient deleted successfully!");
                    getData();
                })
                .catch(error => {
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
                <h6 className="dashboard-title" style={{ color: 'grey' }}>Welcome to Dashboard Admin</h6>
            </div>
            <br />

            <Container className="edit-form">
                <Row>
                    <Col xs={12} md={6}>
                        <h3>Add New Patient</h3>
                        <Form>
                            <Form.Group className="mb-3">
                                <Form.Label>Emri</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter Emri"
                                    value={Emri}
                                    onChange={(e) => setEmri(e.target.value)}
                                />
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Mbiemri</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter Mbiemri"
                                    value={Mbiemri}
                                    onChange={(e) => setMbiemri(e.target.value)}
                                />
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Gjinia</Form.Label>
                                <div>
                                    <input
                                        type="radio"
                                        value="F"
                                        name="gjinia"
                                        checked={Gjinia === 'F'}
                                        onChange={(e) => setGjinia(e.target.value)}
                                    /> Female
                                    <input
                                        type="radio"
                                        value="M"
                                        name="gjinia"
                                        checked={Gjinia === 'M'}
                                        onChange={(e) => setGjinia(e.target.value)}
                                    /> Male
                                </div>
                            </Form.Group>
                            <Form.Group className="mb-3">
                                <Form.Label>Viti i Lindjes</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Enter Viti Lindjes"
                                    value={VitiLindjes}
                                    onChange={(e) => setVitiLindjes(e.target.value)}
                                />
                            </Form.Group>
                            <Button variant="primary" onClick={handleAddPatient}>Add Patient</Button>
                        </Form>
                    </Col>

                    <Col xs={12} md={6}>
                        <h3>Manage Patients</h3>
                        <Table responsive="md">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>User Id</th>
                                    <th>Historiku</th>
                                    <th>Ditelindja</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                {data.length > 0 ? data.map((item, index) => (
                                    <tr key={index}>
                                        <td>{item.id}</td>
                                        <td>{item.historiku}</td>
                                        <td>{item.ditelindja}</td>
                                        <td>
                                            <Button onClick={() => handleEdit(item)} variant="warning" className="me-2">Edit</Button>
                                            <Button onClick={() => handleDelete(item.id)} variant="danger">Delete</Button>
                                        </td>
                                    </tr>
                                )) : (
                                    <tr>
                                        <td colSpan="5" style={{ textAlign: "center" }}>No data available or failed to load data.</td>
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
                            <Form.Label>Emri</Form.Label>
                            <Form.Control
                                type="text"
                                value={Emri}
                                onChange={(e) => setEmri(e.target.value)}
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Mbiemri</Form.Label>
                            <Form.Control
                                type="text"
                                value={Mbiemri}
                                onChange={(e) => setMbiemri(e.target.value)}
                            />
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Gjinia</Form.Label>
                            <div>
                                <input
                                    type="radio"
                                    value="F"
                                    name="gjinia"
                                    checked={Gjinia === 'F'}
                                    onChange={(e) => setGjinia(e.target.value)}
                                /> Female
                                <input
                                    type="radio"
                                    value="M"
                                    name="gjinia"
                                    checked={Gjinia === 'M'}
                                    onChange={(e) => setGjinia(e.target.value)}
                                /> Male
                            </div>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Viti i Lindjes</Form.Label>
                            <Form.Control
                                type="text"
                                value={VitiLindjes}
                                onChange={(e) => setVitiLindjes(e.target.value)}
                            />
                        </Form.Group>
                        <Button variant="primary" onClick={handleAddPatient}>Save Changes</Button>
                    </Form>
                </Modal.Body>
            </Modal>
        </>
    );
};

export default PatientCRUD;
