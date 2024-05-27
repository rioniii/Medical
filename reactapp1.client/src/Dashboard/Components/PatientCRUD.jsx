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
    const [Gjinia, setGjinia] = useState(false);
    const [VitiLindjes, setVitiLindjes] = useState('');

    const [editId, setEditId] = useState('');
    const [editEmri, setEditEmri] = useState('');
    const [editMbiemri, setEditMbiemri] = useState('');
    const [editGjinia, setEditGjinia] = useState(false);
    const [editVitiLindjes, setEditVitiLindjes] = useState('');

    const [data, setData] = useState([]);

    useEffect(() => {
        getData();
    }, []);

    const getData = () => {
        axios.get('https://localhost:7107/api/Pacienti')
            .then((result) => {
                setData(result.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    const handleEdit = (id) => {
        // Handle edit logic
        handleShow();
    }

    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this patient")) {
            // Handle delete logic
            alert(id);
        }
    }

    const handleUpdate = () => {
        // Handle update logic
    }

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
                        <input type="checkbox"
                            checked={Gjinia}
                            onChange={(e) => setGjinia(e.target.checked)} />
                        <label>Gjinia</label>
                    </Col>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter VitiLindjes"
                            value={VitiLindjes} onChange={(e) => setVitiLindjes(e.target.value)} />
                    </Col>
                    <Col>
                        <button className="btn btn-primary">Add Patient</button>
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
                        {
                            data && data.length > 0 ?
                                data.map((item, index) => (
                                    <tr key={index}>
                                        <td>{item.Id}</td>
                                        <td>{item.Emri}</td>
                                        <td>{item.Mbiemri}</td>
                                        <td>{item.Gjinia}</td>
                                        <td>{item.VitiLindjes}</td>
                                        <td>
                                            <span className="me-2">
                                                <Button onClick={() => handleEdit(item.Id)} variant="warning">Edit</Button>
                                            </span>
                                            <span>
                                                <Button onClick={() => handleDelete(item.Id)} variant="danger">Delete</Button>
                                            </span>
                                        </td>
                                    </tr>
                                ))
                                :
                                'Loading...'
                        }
                    </tbody>
                </Table>
            </div>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Modify/Update Patient</Modal.Title>
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
                                <input type="checkbox"
                                    checked={editGjinia}
                                    onChange={(e) => setEditGjinia(e.target.checked)} />
                                <label>Gjinia</label>
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
                </Modal.Footer>
            </Modal>
        </>
    )
}

export default PatientCRUD;
