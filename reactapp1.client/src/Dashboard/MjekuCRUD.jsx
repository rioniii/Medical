import React, { useState, useEffect } from "react";
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
            const response = await axios.get('/api/Mjeket');
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
            await axios.put(`/api/Mjeket/${currentDoctor.id}`, currentDoctor);
        } else {
            await axios.post('/api/Mjeket', currentDoctor);
        }
        fetchDoctors();
        handleClose();
    };

    const handleDelete = async (id) => {
        await axios.delete(`/api/Mjeket/${id}`);
        fetchDoctors();
    };

    return (
        <>
            <Button onClick={() => handleShow()}>Add New Doctor</Button>
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
                                <Button variant="warning" onClick={() => handleShow(doctor)}>Edit</Button>
                                <Button variant="danger" onClick={() => handleDelete(doctor.id)}>Delete</Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <Modal show={showModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>{currentDoctor.id ? 'Edit Doctor' : 'Add Doctor'}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Emri</Form.Label>
                            <Form.Control type="text" name="emri" value={currentDoctor.emri} onChange={handleChange} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Mbiemri</Form.Label>
                            <Form.Control type="text" name="mbiemri" value={currentDoctor.mbiemri} onChange={handleChange} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Reparti</Form.Label>
                            <Form.Control type="text" name="reparti" value={currentDoctor.reparti} onChange={handleChange} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Specializimi</Form.Label>
                            <Form.Control type="text" name="specializimi" value={currentDoctor.specializimi} onChange={handleChange} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Nderrimi</Form.Label>
                            <Form.Control type="text" name="nderrimi" value={currentDoctor.nderrimi} onChange={handleChange} />
                        </Form.Group>
                        <Form.Group>
                            <Form.Label>Angazhimi</Form.Label>
                            <Form.Control type="text" name="angazhimi" value={currentDoctor.angazhimi} onChange={handleChange} />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>Close</Button>
                    <Button variant="primary" onClick={handleSubmit}>{currentDoctor.id ? 'Save Changes' : 'Add Doctor'}</Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

export default MjekuCRUD;