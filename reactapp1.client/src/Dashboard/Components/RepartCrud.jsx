import React, { useState, useEffect } from "react";
import Navigation from "./Navigation.jsx";
import Button from "react-bootstrap/Button";
import Table from 'react-bootstrap/Table';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import axios from 'axios';

const RepartCrud = () => {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [Emri, setEmri] = useState('');
    const [Kati, setKati] = useState('');
    const [NrDhomave, setNrDhomave] = useState('');

    const [editId, setEditId] = useState('');
    const [editEmri, setEditEmri] = useState('');
    const [editKati, setEditKati] = useState('');
    const [editNrDhomave, setEditNrDhomave] = useState('');

    const [data, setData] = useState([]);

    useEffect(() => {
        getData();
    }, []);

    const getData = () => {
        axios.get('https://localhost:7107/api/Repart')
            .then(response => {
                setData(response.data); // Set the fetched data to state
            })
            .catch(error => {
                console.error('Error fetching data:', error);
            });
    };

    const handleEdit = (repart) => {
        setEditId(repart.reparti_Id);
        setEditEmri(repart.emri);
        setEditKati(repart.kati);
        setEditNrDhomave(repart.nrDhomave);
        handleShow();
    };

    const handleUpdate = () => {
        axios.put(`https://localhost:7107/api/Repart/${editId}`, {
            Reparti_Id: editId,
            Emri: editEmri,
            Kati: editKati,
            NrDhomave: editNrDhomave
        })
            .then(() => {
                handleClose();
                getData(); // Refresh data after updating
            })
            .catch(error => console.error('Error:', error));
    };

    const handleAddRepart = () => {
        console.log('Adding Repart:', { Emri, Kati, NrDhomave }); // Debug: log values to be sent
        axios.post('https://localhost:7107/api/Repart', {
            Emri, Kati, NrDhomave
        })
<<<<<<< HEAD
            .then(() => {
                alert('Repart added successfully!'); // Provide UI feedback
                getData(); // Refresh data after adding
                setEmri('');
                setKati('');
                setNrDhomave('');
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Failed to add repart.'); // Provide UI feedback
            });
=======
        .then(() => {
            alert('Repart added successfully!'); // Provide UI feedback
            getData(); // Refresh data after adding
            setEmri('');
            setKati('');
            setNrDhomave('');
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Failed to add repart.'); // Provide UI feedback
        });
>>>>>>> 17ae1912ce7626006e969f7ca2ace8a36b8e2015
    };
    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this repart?")) {
            axios.delete(`https://localhost:7107/api/Repart/${id}`)
                .then(response => {
                    alert("Repart deleted successfully!");
                    getData(); // Refresh the data to reflect the deletion
                })
                .catch(error => {
                    console.error("Error deleting repart:", error);
                    alert("Failed to delete repart.");
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
                        <input type="number" className="form-control" placeholder="Enter Kati"
                            value={Kati} onChange={(e) => setKati(e.target.value)} />
                    </Col>
                    <Col>
                        <input type="number" className="form-control" placeholder="Enter NrDhomave"
                            value={NrDhomave} onChange={(e) => setNrDhomave(e.target.value)} />
                    </Col>
                    <Col>
                        <button className="btn btn-primary" onClick={handleAddRepart}>Add Repart</button>
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
                            <th>Kati</th>
                            <th>NrDhomave</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.length > 0 ? data.map((item, index) => (
                            <tr key={index}>
                                <td>{item.reparti_Id}</td>
                                <td>{item.emri}</td>
                                <td>{item.kati}</td>
                                <td>{item.nrDhomave}</td>
                                <td>
                                    <span className="me-2">
                                        <Button onClick={() => handleEdit(item)} variant="warning">Edit</Button>
                                    </span>
                                    <span>
                                        <Button onClick={() => handleDelete(item.reparti_Id)} variant="danger">Delete</Button>
                                    </span>
                                </td>
                            </tr>
                        )) : (
                            <tr>
                                <td colSpan="5" style={{ textAlign: "center" }}>No data available or failed to load data.</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Update Repart</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Container className="edit-form">
                        <Row>
                            <Col>
                                <input type="text" className="form-control" placeholder="Enter Emri"
                                    value={editEmri} onChange={(e) => setEditEmri(e.target.value)} />
                            </Col>
                            <Col>
                                <input type="number" className="form-control" placeholder="Enter Kati"
                                    value={editKati} onChange={(e) => setEditKati(e.target.value)} />
                            </Col>
                            <Col>
                                <input type="number" className="form-control" placeholder="Enter NrDhomave"
                                    value={editNrDhomave} onChange={(e) => setEditNrDhomave(e.target.value)} />
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
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default RepartCrud;
