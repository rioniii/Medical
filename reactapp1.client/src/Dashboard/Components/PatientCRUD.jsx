import React, { useState,useEffect} from "react"
import Navigation from "./Navigation.jsx";
import Button from "react-bootstrap/Button";
import Table from 'react-bootstrap/Table'
import Actions from './Actions.jsx'
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import axios from 'axios';

const PatientCRUD = () => {

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const [Name, setName] = useState('');
    const [Surname, setSurname] = useState('');
    const [isRegistered, setIsRegistered] = useState(false);
    const [age, setAge] = useState('');

    const [editId, setEditId] = useState('');
    const [editName, setEditName] = useState('');
    const [editSurname, setEditSurname] = useState('');
    const [editIsRegistered, setEditIsRegistered] = useState(false);
    const [editAge, setEditAge] = useState('');


    const patientData = [
        {
            Id: 1,
            Name: 'Filan',
            Surname: 'Fisteku',
            isRegistered: 1,
            age: 20
        },
        {
            Id: 2,
            Name: 'Filane',
            Surname: 'Gashi',
            isRegistered: 0,
            age: 23
        },
        {
            Id: 3,
            Name: 'Arta',
            Surname: 'Gashi',
            isRegistered: 1,
            age: 30
        }


    ]
    const [data, setData] = useState([]);

    useEffect(() => {
        getData();
    }, [])

    const getData = () => {
        axios.get('https://localhost:7107/api/Patient')
            .then((result) => {
                setData(result.data)
            })
            .catch((error) => {
                console.log(error)
            })  
    }
    const putData = () => {
        axios.get('https://localhost:7107/api/Patient')
            .then((result) => {
                setData(result.data)
            })
            .catch((error) => {
                console.log(error)
            })  
    }

    const handleEdit = (id) => {
        handleShow();
    }
    const handleDelete = (id) => {
        if (window.confirm("Are you sure you want to delete this patient") == true) {
            alert(id);
        }
    }
    const handleUpdate = () => {
       
    }

    return (
        <>
            <Navigation />

            <br></br>
            <div>
                <h2 className="dashboard-title">Dashboard</h2>
                <h6 className="dashboard-title" style={{ color: 'grey' }}>Welcome to Dashboard Admin</h6>
            </div>
            <br></br>

            <Container className="edit-form">
                <Row>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter Name" 
                            value={Name} onChange={() => setName(e.target.value)} />
                    </Col>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter Surname"
                        value={Surname} onChange={() => setSurname(e.target.value)}/>
                    </Col>
                    <Col>
                        <input type="checkbox"
                            checked={isRegistered === 1 ? true : false} 
                        onChange = {(e) => handleEdit(e)} value={isRegistered}/>
                        <label>IsRegistered</label>
                    </Col>
                    <Col>
                        <input type="text" className="form-control" placeholder="Enter Age"
                        value={age} onChange={() => setAge(e.target.value)}/>
                    </Col>
                    <Col>
                        <button className="btn btn-primary">Add Patient</button>
                    </Col>

                </Row>
            </Container>
            <br></br>

            <div>

                <Table responsive="md">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>isRegistered</th>
                            <th>Age</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>

                        {
                            data && data.length > 0 ?
                                data.map((item, index) => {
                                    return (
                                        <tr key={index}>
                                            <td>{item.Id}</td>
                                            <td>{item.Name}</td>
                                            <td>{item.Surname}</td>
                                            <td>{item.isRegistered}</td>
                                            <td>{item.age}</td>
                                            <td>
                                                <span className="me-2">
                                                    <Button onClick={() => handleEdit(item.Id)} variant="warning">Edit</Button>
                                                </span>
                                                <span>
                                                    <Button onClick={()=> handleDelete(item.Id)} variant="danger">Delete</Button>
                                                </span>
                                            </td>
                                        </tr>
                                    )
                                })
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
                                <input type="text" className="form-control" placeholder="Enter Name" 
                                    value={editName} onChange={() => setEditName(e.target.value)} />
                            </Col>
                            <Col>
                                <input type="text" className="form-control" placeholder="Enter Surname" 
                                    value={editSurname} onChange={() => setEditSurname(e.target.value)} />
                            </Col>
                            <Col>
                                <input type="checkbox"
                                    checked={editIsRegistered === 1 ? true : false} 
                                    onChange={(e) => setEditIsRegistered(e)} value={editIsRegistered} />
                                <label>IsRegistered</label>
                            </Col>
                            <Col>
                                <input type="text" className="form-control" placeholder="Enter Age"
                                value={editAge} onChange={() => setEditAge(e.target.value)}/>
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