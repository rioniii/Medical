import React, { useState, useEffect } from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Nav from 'react-bootstrap/Nav';
import PropTypes from 'prop-types';
import './Header.css';

const Header = () => {
    const [isDoctor, setIsDoctor] = useState(false);
    const token = localStorage.getItem('token');
    const userRoles = JSON.parse(localStorage.getItem('userRoles')) || [];
    const navigate = useNavigate();

    useEffect(() => {
        // Check if user has 'Doctor' role
        setIsDoctor(userRoles.includes('Doctor'));
    }, [userRoles]);

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('userRoles');
        navigate('/LoginForm');
    };

    const handleDashboardClick = () => {
        // Redirect to PatientCRUD page if user is a doctor
        navigate('/PatientCRUD');
    };

    return (
        <header>
            <Navbar bg="dark" variant="dark" expand="lg" className="shadow">
                <Container fluid>
                    <Navbar.Brand href="#/">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Nav.Link as={NavLink} to="/" className="nav-item">Home</Nav.Link>
                            <Nav.Link as={NavLink} to="/AboutUs" className="nav-item">About</Nav.Link>
                            <Nav.Link as={NavLink} to="/Contact" className="nav-item">Contact</Nav.Link>
                            {token ? (
                                <Button variant="danger" onClick={handleLogout}>Log Out</Button>
                            ) : (
                                <>
                                    <Nav.Link as={NavLink} to="/RegisterForm" className="nav-item">Register</Nav.Link>
                                    <Nav.Link as={NavLink} to="/LoginForm" className="nav-item">Login</Nav.Link>
                                </>
                            )}
                        </Nav>
                        {isDoctor && (
                            <Button
                                variant="info"
                                onClick={handleDashboardClick} // Redirect on click
                                className="ms-auto"
                            >
                                Dashboard
                            </Button>
                        )}
                        <Button variant="success" className="ms-2">Make an Appointment</Button>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
};

Header.propTypes = {
    userRoles: PropTypes.array,
};

export default Header;
