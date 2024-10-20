import React, { useState, useEffect } from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Nav from 'react-bootstrap/Nav';
import './Header.css'; // Make sure this file exists

const Header = ({ userRole }) => {
    const [isDoctor, setIsDoctor] = useState(false);

    useEffect(() => {
        // Check the user role and update state
        if (userRole === 'Doctor') {
            setIsDoctor(true);
        } else {
            setIsDoctor(false);
        }
    }, [userRole]);

    return (
        <header>
            <Navbar bg="dark" variant="dark" expand="lg" className="shadow">
                <Container fluid>
                    <Navbar.Brand href="./Ballina.jsx">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto" style={{ margin: "auto" }}>
                            <Nav.Link href="/" className="nav-item">Home</Nav.Link>
                            <Nav.Link href="#AboutUs" className="nav-item">About</Nav.Link>
                            <Nav.Link href="#Clinic" className="nav-item">Clinic</Nav.Link>
                            <Nav.Link href="#Doctors" className="nav-item">Doctors</Nav.Link>
                            <Nav.Link href="#Contact" className="nav-item">Contact</Nav.Link>
                            <Nav.Link href="#RegisterForm" className="nav-item">Register</Nav.Link>
                            <Nav.Link href="#LoginForm" className="nav-item">Login</Nav.Link>
                            {/* Conditionally render Dashboard link */}
                            {isDoctor && (
                                <Nav.Link href="#PatientCRUD" className="nav-item">Dashboard</Nav.Link>
                            )}
                        </Nav>
                        <Button variant="success" className="ms-auto">Make an Appointment</Button>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
};

export default Header;
