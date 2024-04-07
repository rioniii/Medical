import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';

function Header() {
    return (
        <header>
            <Navbar sticky="top" bg="dark" variant="dark" expand="lg" className="navbar">
                <Container>
                    <Navbar.Brand href="#home">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="mx-auto">
                            <Nav.Link className="links" href="#home">Home</Nav.Link>
                            <Nav.Link className="links" href="#about">About</Nav.Link>
                            <Nav.Link className="links" href="#klinika">Klinika</Nav.Link>
                            <Nav.Link className="links" href="#doktoret">Doktoret</Nav.Link>
                            <Nav.Link className="links" href="#services">Services</Nav.Link>
                            <Nav.Link className="links" href="#">Blog</Nav.Link>
                            <Nav.Link className="links" href="#" >Contact</Nav.Link> 
                        </Nav>
                        <Button variant="success">Make an Appointment</Button>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default Header;
