import React, { Component } from 'react';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
import i4 from './assets/i4.jpg'; import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Route, NavLink, Routes, HashRouter } from 'react-router-dom';
const RegisterForm = React.lazy(() => import("./RegisterForm"));
const LoginForm = React.lazy(() => import("./LoginForm"));
  const Header = () => {
      const images = [i3, i2, i4];
      return (
          <div>
              <header>
                  <Navbar bg="dark" data-bs-theme="dark">
                      <Container className="nav">
                          <Navbar.Brand href="#home">Medical</Navbar.Brand>
                          <Navbar.Toggle aria-controls="basic-navbar-nav" />
                          <Navbar.Collapse id="basic-navbar-nav">
                              <Nav className="me-auto ms-auto">
                                  <NavLink to="/">Home</NavLink>
                                  <NavLink to="/">About</NavLink>
                                  <NavLink to="/">Service</NavLink>
                                  <NavLink to="/">Contact</NavLink>
                                  <NavLink to="/RegisterForm">Register</NavLink>
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
              <IntervalSlider images={images} interval={3000} />
              <Card />
              <Footer />
          </div>
      );
             
}
class App extends Component {
    render() {
    return( 
            <div>
                <HashRouter>
                <Routes>
                    <Route path="/" element={<Header />}></Route>
                    <Route path="/RegisterForm" element={<RegisterForm />}></Route>
                    <Route path="/LoginForm" element={<LoginForm />}></Route>
                    </Routes>
                </HashRouter >
               </div>
    );
        }
}

export default App; 