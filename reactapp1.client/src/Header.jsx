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
            <Navbar sticky="top" bg="dark" variant="dark" expand="lg">
                <Container>
                    <Navbar.Brand href="#home">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="mx-auto">
                            <Nav.Link href="#home">Home</Nav.Link>
                            <Nav.Link href="#about" className="mr-2">About</Nav.Link>
                            <Nav.Link href="#klinika" className="mr-2">Klinika</Nav.Link>
                            <Nav.Link href="#doktoret" className="mr-2">Doktoret</Nav.Link>
                            <Nav.Link href="#services" className="mr-2">Services</Nav.Link>
                            <Nav.Link href="#" className="mr-2">Blog</Nav.Link>
                            <Nav.Link href="#" className="mr-2">Contact</Nav.Link> 
                        </Nav>
                        <Button variant="success">Make an Appointment</Button>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default Header;
