import React, { useState, useEffect } from "react";
import { Container, Row, Col, Button } from 'react-bootstrap';
import axios from 'axios';
import { Button, Modal, Table, Form } from 'react-bootstrap';

const MjekuCRUD = () => {
    const [doctors, setDoctors] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [currentDoctor, setCurrentDoctor] = useState({ id: '', emri: '', mbiemri: '', reparti: '', specializimi: '', nderrimi: '', angazhimi: '' });

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

    const handleShow = (doctor = { id: '', emri: '', mbiemri: '', reparti: '', specializimi: '', nderrimi: '', angazhimi: '' }) => {
        setCurrentDoctor(doctor);
        setShowModal(true);
    };

    const handleClose = () => {
        setShowModal(false);
    };

    const handleChange = (e) => {
        setCurrentDoctor({ ...currentDoctor, [e.target.name]: e.target.value });
    };

    const handleSubmit = async () => {
        if (currentDoctor.id) {
            await axios.put(`https://localhost:7107/api/Mjeku/${currentDoctor.id}`, currentDoctor);
        } else {
            await axios.post('https://localhost:7107/api/Mjeku', currentDoctor);
        }
        fetchDoctors();
        handleClose();
    };

    const handleDelete = async (id) => {
        if (window.confirm("Are you sure you want to delete this Doctor?")) {
            await axios.delete(`https://localhost:7107/api/Mjeku/${id}`)
                .then(response => {
                    alert("Doctor deleted successfully!");
                    fetchDoctors(); // Refresh the data to reflect the deletion
                })
                .catch(error => {
                    console.error("Error deleting doctor:", error);
                    alert("Failed to delete doctor.");
                });
        }
    };


    return (
        <>
            <Button onClick={() => handleShow()}>Add New Doctor</Button>
            <Container>
                {doctors.map(doctor => (
                    <Row key={doctor.id} className="mb-3">
                        <Col>{doctor.emri}</Col>
                        <Col>{doctor.mbiemri}</Col>
                        <Col>{doctor.reparti}</Col>
                        <Col>{doctor.specializimi}</Col>
                        <Col>{doctor.nderrimi}</Col>
                        <Col>{doctor.angazhimi}</Col>
                        <Col>
                            <Button variant="warning" onClick={() => handleShow(doctor)}>Edit</Button>
                            <Button variant="danger" onClick={() => handleDelete(doctor.id)}>Delete</Button>
                        </Col>
                    </Row>
                ))}
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
                    <Button variant="secondary" onClick={handleClose}>Close</Button>
                    <Button variant="primary" onClick={handleSubmit}>{currentDoctor.id ? 'Save Changes' : 'Add Doctor'}</Button>
                </Modal.Footer>
            </Modal>
        </>
    );
  }
export default MjekuCRUD;