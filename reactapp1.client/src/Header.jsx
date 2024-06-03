import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import ListGroup from 'react-bootstrap/ListGroup';

const Header = () => {
    return (
        <header>
            <Navbar bg="dark" variant="dark" expand="lg">
                <Container fluid>
                    <Navbar.Brand href="./Ballina.jsx">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav" >
                        <ListGroup className="NavList mb-2 mb-lg-0" horizontal="lg" style={{
                            margin: "auto",
                            display: "flex",
                            justifyContent: "center"
                        }}>
                            <ListGroup.Item variant="success" action href="#Ballina">Home</ListGroup.Item>
                            <ListGroup.Item variant="success" action href="#AboutUs">About Us</ListGroup.Item>
                            <ListGroup.Item variant="success" action href="#Clinic">Clinic</ListGroup.Item>
                            <ListGroup.Item variant="success" action href="#Doctors">Doctors</ListGroup.Item>
                            <ListGroup.Item variant="success" action href="#Contact">Contact</ListGroup.Item>
                            <ListGroup.Item variant="success" action href="#RegisterForm">Register</ListGroup.Item>
                            <ListGroup.Item variant="success" action href="#LoginForm">Login</ListGroup.Item>
                        </ListGroup>
                        <Button variant="success" className="ms-auto">Make an appointment</Button>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default Header;
