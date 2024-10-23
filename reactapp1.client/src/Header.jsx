import React, { useState, useEffect } from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Nav from 'react-bootstrap/Nav';
import './Header.css';

const Header = ({ userRole }) => {
    const [isDoctor, setIsDoctor] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);
    const token = localStorage.getItem('token');
    const navigate = useNavigate();

    useEffect(() => {
        setIsAdmin(userRole === 'Admin');
        setIsDoctor(userRole === 'Doctor');
        console.log("User Role in Header:", userRole); // Log userRole for debugging
    }, [userRole]);

    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate('/LoginForm');
    };

    return (
        <header>
            <Navbar bg="dark" variant="dark" expand="lg" className="shadow">
                <Container fluid>
                    <Navbar.Brand href="#/">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto" style={{ margin: "auto" }}>
                            <Nav.Link as={NavLink} to="/" className="nav-item">Home</Nav.Link>
                            <Nav.Link as={NavLink} to="/AboutUs" className="nav-item">About</Nav.Link>
                            <Nav.Link as={NavLink} to="/Contact" className="nav-item">Contact</Nav.Link>
                            {isAdmin && (
                                <Nav.Link as={NavLink} to="/admin-dashboard" className="nav-item">Admin Dashboard</Nav.Link>
                            )}
                            {isDoctor && (
                                <Nav.Link as={NavLink} to="/PatientCRUD" className="nav-item">Doctor Dashboard</Nav.Link>
                            )}
                            {token ? (
                                <Button variant="danger" onClick={handleLogout}>Log Out</Button>
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
