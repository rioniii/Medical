import React, { Suspense } from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Route, NavLink, Routes, HashRouter } from 'react-router-dom';
const RegisterForm = React.lazy(() => import("./RegisterForm"));
function Header() {
    
    return (
        <>
        <header>
            <Navbar bg="dark" data-bs-theme="dark">
                <Container className="nav">
                    <Navbar.Brand href="#home">Medical</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto ms-auto">
                            <NavLink to="/">Home</NavLink>
                            <NavLink to="/">About</NavLink>
                            <NavLink to="/RegisterForm">Services</NavLink>
                            <NavLink to="/">Contact</NavLink>

                            <NavDropdown title="Dropdown" id="basic-nav-dropdown">
                                <NavDropdown.Item href="#action/3.1">Services</NavDropdown.Item>
                                <NavDropdown.Item href="#action/3.2">Another action</NavDropdown.Item>

                            </NavDropdown>
                            <Button style={{ marginLeft: '290px' }} variant="outline-success">Make an appointment</Button>{' '}
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </header>
        <HashRouter>
          <Routes>
             <Route path="/RegisterForm" element={<RegisterForm />}></Route>
          </Routes>
        </HashRouter>
        </>
    );
}
export default Header; 