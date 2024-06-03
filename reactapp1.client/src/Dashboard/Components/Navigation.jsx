import React from 'react';
import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import { Dropdown } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

function Navigation() {
    return (
        <Navbar bg="dark" data-bs-theme="dark" className="bg-body-tertiary" >
            <Container>
                <Navbar.Brand>Medical</Navbar.Brand>
                <Navbar.Toggle />
                <Navbar.Collapse className="justify-content-end">
                    <Dropdown>
                        <Dropdown.Toggle variant="success" id="dropdown-basic">
                            CRUD Operations
                        </Dropdown.Toggle>

                        <Dropdown.Menu>
                            <Dropdown.Item as={Link} to="/PatientCRUD">Pacient CRUD</Dropdown.Item>
                            <Dropdown.Item as={Link} href = '/MjekuCRUD'>Mjeku CRUD</Dropdown.Item>
                            <Dropdown.Item as={Link} to="/other-crud">Other CRUD</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>
                    <Navbar.Text>
                        Signed in as: <a>Rion</a>
                    </Navbar.Text>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Navigation;
