import React, { useState, useEffect } from 'react';
import { NavLink, useNavigate } from 'react-router-dom'; // Add react-router-dom for navigation
import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Nav from 'react-bootstrap/Nav';
import './Header.css';

const Header = ({ userRole }) => {
    const [isDoctor, setIsDoctor] = useState(false);
    const token = localStorage.getItem('token'); // Check if user is logged in
    const navigate = useNavigate(); // Initialize navigation

    useEffect(() => {
        if (userRole === 'Doctor') {
            setIsDoctor(true);
        } else {
            setIsDoctor(false);
        }
    }, [userRole]);

    const handleLogout = () => {
        localStorage.removeItem('token'); // Clear token on logout
        navigate('/LoginForm'); // Redirect to login form after logout
    };

    return (
        <header>
            <Navbar bg="dark" variant="dark" expand="lg" className="shadow">
                <Container fluid>
                    <Navbar.Brand href="./Ballina.jsx">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto" style={{ margin: "auto" }}>
                            <Nav.Link as={NavLink} to="/" className="nav-item">Home</Nav.Link>
                            <Nav.Link as={NavLink} to="/AboutUs" className="nav-item">About</Nav.Link>
                            <Nav.Link as={NavLink} to="/Clinic" className="nav-item">Clinic</Nav.Link>
                            <Nav.Link as={NavLink} to="/Doctors" className="nav-item">Doctors</Nav.Link>
                            <Nav.Link as={NavLink} to="/Contact" className="nav-item">Contact</Nav.Link>
                            {/* Conditionally render links based on login status */}
                            {token ? (
                                <>
                                    {isDoctor && (
                                        <Nav.Link as={NavLink} to="/PatientCRUD" className="nav-item">Dashboard</Nav.Link>
                                    )}
                                    <Button variant="danger" onClick={handleLogout}>Log Out</Button>
                                </>
                            ) : (
                                <>
                                    <Nav.Link as={NavLink} to="/RegisterForm" className="nav-item">Register</Nav.Link>
                                    <Nav.Link as={NavLink} to="/LoginForm" className="nav-item">Login</Nav.Link>
                                </>
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
