import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Modal, Table, Form } from 'react-bootstrap';

const PatientCRUD = () => {
    // State for storing data
    const [data, setData] = useState([]);


    // States for adding new patient
    const [UserId, setUserId] = useState('');
    const [Historiku, setHistoriku] = useState('');
    const [Ditelindja, setDitelindja] = useState('');

    // States for editing an existing patient
    const [editId, setEditId] = useState('');
    const [editUserId, setEditUserId] = useState('');
    const [editHistoriku, setEditHistoriku] = useState('');
    const [editDitelindja, setEditDitelindja] = useState('');

    // State to manage modal visibility
    const [show, setShow] = useState(false);

    // Fetch patients data on component load
    useEffect(() => {
        getData();
    }, []);

    // Function to fetch data
    const getData = () => {
        axios.get('https://localhost:7107/api/Pacienti')
            .then(response => {
                setData(response.data);
            })
            .catch(error => console.error('Error fetching data:', error));
    };

    // Function to handle new patient addition
    const handleAddPatient = () => {
        const payload = {
            UserId,
            Historiku,
            Ditelindja,
        };
        console.log("Payload:", payload);  // Log to verify payload structure

        axios.post('https://localhost:7107/api/Pacienti', payload)
            .then(() => {
                getData(); // Refresh data after adding
                setUserId('');
                setHistoriku('');
                setDitelindja('');
            })
            .catch(error => console.error('Error adding patient:', error));
    };


    // Function to open edit modal and populate fields
    const handleEdit = (patient) => {
        setEditId(patient.id);
        setEditUserId(patient.userId);
        setEditHistoriku(patient.historiku);
        setEditDitelindja(patient.ditelindja);
        handleShow();
    };

    // Function to update existing patient
    const handleUpdate = () => {
        axios.put(`https://localhost:7107/api/Pacienti/${editId}`, {
            Id: editId,
            UserId: editUserId,
            Historiku: editHistoriku,
            Ditelindja: editDitelindja,
        })
            .then(() => {
                handleClose();
                getData();
            })
            .catch(error => console.error('Error updating patient:', error));
    };

    // Function to delete a patient
    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this patient?")) {
            axios.delete(`https://localhost:7107/api/Pacienti/${id}`)
                .then(() => {
                    alert("Patient deleted successfully!");
                    getData();
                })
                .catch(error => {
                    console.error("Error deleting patient:", error);
                    alert("Failed to delete patient.");
                });
        }
    };

    // Modal visibility handlers
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <div>
            <h1>Manage Patients</h1>

            <Form>
                <Form.Group className="mb-3">
                    <Form.Label>User ID</Form.Label>
                    <Form.Control type="text" value={UserId} onChange={e => setUserId(e.target.value)} />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Historiku</Form.Label>
                    <Form.Control type="text" value={Historiku} onChange={e => setHistoriku(e.target.value)} />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Ditelindja</Form.Label>
                    <Form.Control type="date" value={Ditelindja} onChange={e => setDitelindja(e.target.value)} />
                </Form.Group>
                <Button variant="primary" onClick={handleAddPatient}>Add Patient</Button>
            </Form>

            <Table responsive="md" className="mt-4">
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
                            <td>{item.userId}</td>
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

            {/* Modal for editing patient */}
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit Patient</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className="mb-3">
                        <Form.Label>User ID</Form.Label>
                        <Form.Control type="text" value={editUserId} onChange={e => setEditUserId(e.target.value)} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Historiku</Form.Label>
                        <Form.Control type="text" value={editHistoriku} onChange={e => setEditHistoriku(e.target.value)} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Ditelindja</Form.Label>
                        <Form.Control type="date" value={editDitelindja} onChange={e => setEditDitelindja(e.target.value)} />
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>Close</Button>
                    <Button variant="primary" onClick={handleUpdate}>Save Changes</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default PatientCRUD;
