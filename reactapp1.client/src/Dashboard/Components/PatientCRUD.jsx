import React, { useState, useEffect } from "react";
import Navigation from "./Navigation.jsx";
import Button from "react-bootstrap/Button";
import Table from 'react-bootstrap/Table';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import axios from 'axios';

const PatientCRUD = () => {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [Emri, setEmri] = useState('');
    const [Mbiemri, setMbiemri] = useState('');
    const [Gjinia, setGjinia] = useState('M');
    const [VitiLindjes, setVitiLindjes] = useState('');

    const [editId, setEditId] = useState('');
    const [editEmri, setEditEmri] = useState('');
    const [editMbiemri, setEditMbiemri] = useState('');
    const [editGjinia, setEditGjinia] = useState('M');
    const [editVitiLindjes, setEditVitiLindjes] = useState('');

    const [data, setData] = useState([]);

    useEffect(() => {
        getData();
    }, []);

    const getData = () => {
        axios.get('https://localhost:7107/api/Pacienti')
            .then(response => {
                setData(response.data); // Set the fetched data to state
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    };

    const handleEdit = (patient) => {
        setEditId(patient.patient_Id);
        setEditEmri(patient.emri);
        setEditMbiemri(patient.mbiemri);
        setEditGjinia(patient.gjinia);
        setEditVitiLindjes(patient.vitiLindjes);
        handleShow();
    };



    const handleUpdate = () => {
        axios.put(`https://localhost:7107/api/Pacienti/${editId}`, {
            Patient_Id: editId,
            Emri: editEmri,
            Mbiemri: editMbiemri,
            Gjinia: editGjinia
        })
            .then(() => {
                handleClose();
                getData(); // Refresh data after updating
            })
            .catch(error => console.error('Error:', error));
    };

    const handleAddPatient = () => {
        axios.post('https://localhost:7107/api/Pacienti', {
            Emri, Mbiemri, Gjinia, VitiLindjes
        })
            .then(() => {
                getData(); // Refresh data after adding
                setEmri('');
                setMbiemri('');
                setGjinia('M'); // Reset to default
                setVitiLindjes('');
            })
            .catch(error => console.error('Error:', error));
    };

    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this patient?")) {
            axios.delete(`https://localhost:7107/api/Pacienti/${id}`)
                .then(response => {
                    alert("Patient deleted successfully!");
                    getData(); // Refresh the data to reflect the deletion
                })
                .catch(error => {
                    console.error("Error deleting patient:", error);
                    alert("Failed to delete patient.");
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
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter Emri"
                            value={Emri} onChange={(e) => setEmri(e.target.value)} />
                    </Col>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter Mbiemri"
                            value={Mbiemri} onChange={(e) => setMbiemri(e.target.value)} />
                    </Col>
                    <Col>
                        <input
                            type="radio"
                            value="F"
                            name="gjinia"
                            checked={Gjinia === 'F'}
                            onChange={(e) => setGjinia(e.target.value)}
                        /> F   
                        <input
                            type="radio"
                            value="M"
                            name="gjinia"
                            checked={Gjinia === 'M'}
                            onChange={(e) => setGjinia(e.target.value)}
                        /> M
                    </Col>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter VitiLindjes"
                            value={VitiLindjes} onChange={(e) => setVitiLindjes(e.target.value)} />
                    </Col>
                    <Col>
                        <button className="btn btn-primary" onClick={handleAddPatient}>Add Patient</button>
                    </Col>
                </Row>
            </Container>
            <br />

            <div>
                <Table responsive="md">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Emri</th>
                            <th>Mbiemri</th>
                            <th>Gjinia</th>
                            <th>VitiLindjes</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.length > 0 ? data.map((item, index) => (
                            <tr key={index}>
                                <td>{item.patient_Id}</td>
                                <td>{item.emri}</td>
                                <td>{item.mbiemri}</td>
                                <td>{item.gjinia}</td>
                                <td>{item.vitiLindjes}</td>
                                <td>
                                    <span className="me-2">
                                        <Button onClick={() => handleEdit(item)} variant="warning">Edit</Button>
                                    </span>
                                    <span>
                                        <Button onClick={() => handleDelete(item.patient_Id)} variant="danger">Delete</Button>
                                    </span>
                                </td>
                            </tr>
                        )) : (
                            <tr>
                                <td colSpan="6" style={{ textAlign: "center" }}>No data available or failed to load data.</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Patient</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Container className="edit-form">
                        <Row>
                            <Col>
                                <input type="text" className="form-control" placeholder="Enter Emri"
                                    value={editEmri} onChange={(e) => setEditEmri(e.target.value)} />
                            </Col>
                            <Col>
                                <input type="text" className="form-control" placeholder="Enter Mbiemri"
                                    value={editMbiemri} onChange={(e) => setEditMbiemri(e.target.value)} />
                            </Col>
                            <Col>
                                <label>
                                    <input
                                        type="radio"
                                        name="gjinia"
                                        value="M"
                                        checked={editGjinia === 'M'}
                                        onChange={(e) => setEditGjinia(e.target.value)}
                                    />
                                    Male
                                </label>
                                <label>
                                    <input
                                        type="radio"
                                        name="gjinia"
                                        value="F"
                                        checked={editGjinia === 'F'}
                                        onChange={(e) => setEditGjinia(e.target.value)}
                                    />
                                    Female
                                </label>
                            </Col>
                            <Col>
                                <input type="text" className="form-control" placeholder="Enter VitiLindjes"
                                    value={editVitiLindjes} onChange={(e) => setEditVitiLindjes(e.target.value)} />
                            </Col>
                        </Row>
                    </Container>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleUpdate}>
                        Save Changes
                    </Button>
                    <Button></Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default PatientCRUD;