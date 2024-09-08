import React, { useState, useEffect } from "react";
import { Container, Row, Col, Button as BootstrapButton } from 'react-bootstrap';
import axios from 'axios';
import Table from 'react-bootstrap/Table';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import Navigation from "./Navigation";

const MjekuCRUD = () => {
    const [doctors, setDoctors] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [currentDoctor, setCurrentDoctor] = useState({
        id: '',
        emri: '',
        mbiemri: '',
        reparti: '',
        specializimi: '',
        nderrimi: '',
        angazhimi: ''
    });

    useEffect(() => {
        fetchDoctors();
    }, []);

    const fetchDoctors = async () => {
        try {
            const response = await axios.get('https://localhost:7107/api/Mjeku');
            setDoctors(response.data);
        } catch (error) {
            console.error('Error fetching doctors:', error);
        }
    };

    const handleShow = (doctor = {
        id: '',
        emri: '',
        mbiemri: '',
        reparti: '',
        specializimi: '',
        nderrimi: '',
        angazhimi: ''
    }) => {
        setCurrentDoctor(doctor);
        setShowModal(true);
    };

    const handleClose = () => {
        setShowModal(false);
        setCurrentDoctor({
            id: '',
            emri: '',
            mbiemri: '',
            reparti: '',
            specializimi: '',
            nderrimi: '',
            angazhimi: ''
        });
    };

    const handleChange = (e) => {
        setCurrentDoctor({
            ...currentDoctor,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async () => {
        try {
            if (currentDoctor.id) {
                await axios.put(`https://localhost:7107/api/Mjeku/${currentDoctor.id}`, currentDoctor);
            } else {
                await axios.post('https://localhost:7107/api/Mjeku', currentDoctor);
            }
            fetchDoctors();
            handleClose();
        } catch (error) {
            console.error('Error saving doctor:', error);
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this Doctor?")) {
            try {
                await axios.delete(`https://localhost:7107/api/Mjeku/${id}`);
                alert("Doctor deleted successfully!");
                fetchDoctors(); // Refresh the data to reflect the deletion
            } catch (error) {
                console.error("Error deleting doctor:", error);
                alert("Failed to delete doctor.");
            }
        }
    };

    return (
        <>
            <Navigation />
            <BootstrapButton onClick={() => handleShow()}>Add New Doctor</BootstrapButton>
            <Container>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Emri</th>
                            <th>Mbiemri</th>
                            <th>Reparti</th>
                            <th>Specializimi</th>
                            <th>Nderrimi</th>
                            <th>Angazhimi</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {doctors.map(doctor => (
                            <tr key={doctor.id}>
                                <td>{doctor.emri}</td>
                                <td>{doctor.mbiemri}</td>
                                <td>{doctor.reparti}</td>
                                <td>{doctor.specializimi}</td>
                                <td>{doctor.nderrimi}</td>
                                <td>{doctor.angazhimi}</td>
                                <td>
                                    <BootstrapButton variant="warning" onClick={() => handleShow(doctor)}>Edit</BootstrapButton>
                                    <BootstrapButton variant="danger" onClick={() => handleDelete(doctor.id)}>Delete</BootstrapButton>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            </Container>

            <Modal show={showModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{currentDoctor.id ? 'Edit Doctor' : 'Add Doctor'}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Row>
                            <Col>
                                <Form.Group>
                                    <Form.Label>Emri</Form.Label>
                                    <Form.Control type="text" name="emri" value={currentDoctor.emri} onChange={handleChange} />
                                </Form.Group>
                            </Col>
                            <Col>
                                <Form.Group>
                                    <Form.Label>Mbiemri</Form.Label>
                                    <Form.Control type="text" name="mbiemri" value={currentDoctor.mbiemri} onChange={handleChange} />
                                </Form.Group>
                            </Col>
                            <Col>
                                <Form.Group>
                                    <Form.Label>Reparti</Form.Label>
                                    <Form.Control type="text" name="reparti" value={currentDoctor.reparti} onChange={handleChange} />
                                </Form.Group>
                            </Col>
                            <Col>
                                <Form.Group>
                                    <Form.Label>Specializimi</Form.Label>
                                    <Form.Control type="text" name="specializimi" value={currentDoctor.specializimi} onChange={handleChange} />
                                </Form.Group>
                            </Col>
                            <Col>
                                <Form.Group>
                                    <Form.Label>Nderrimi</Form.Label>
                                    <Form.Control type="text" name="nderrimi" value={currentDoctor.nderrimi} onChange={handleChange} />
                                </Form.Group>
                            </Col>
                            <Col>
                                <Form.Group>
                                    <Form.Label>Angazhimi</Form.Label>
                                    <Form.Control type="text" name="angazhimi" value={currentDoctor.angazhimi} onChange={handleChange} />
                                </Form.Group>
                            </Col>
                        </Row>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <BootstrapButton variant="secondary" onClick={handleClose}>Close</BootstrapButton>
                    <BootstrapButton variant="primary" onClick={handleSubmit}>
                        {currentDoctor.id ? 'Save Changes' : 'Add Doctor'}
                    </BootstrapButton>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default MjekuCRUD;
