import React, { Component, Suspense } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';
import PatientCRUD from './PatientCRUD'
import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import Button from 'react-bootstrap/Button';
import ListGroup from 'react-bootstrap/ListGroup';
import { Route, Routes, HashRouter } from 'react-router-dom';
import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';


 
const RegisterForm = React.lazy(() => import("./RegisterForm"));
const LoginForm = React.lazy(() => import("./LoginForm"));
const AboutUs = React.lazy(() => import("./AboutUs"));
const Header = () => {
    const images = [i2, i4, i3];



      return (
          <div>
              <header>
                  <Navbar bg="dark" variant="dark" expand="lg">
                      <Container fluid>
                          <Navbar.Brand href="/">

                                Medical
                          </Navbar.Brand>
                          <Navbar.Toggle aria-controls="basic-navbar-nav" />
                          <Navbar.Collapse id="basic-navbar-nav">
                              <ListGroup className="NavList mb-2 mb-lg-0" horizontal="lg">
                                  <ListGroup.Item className="item" action variant="success" href="/">Home</ListGroup.Item>
                                  <ListGroup.Item variant="success" action href="#AboutUs">About Us</ListGroup.Item>
                                  <ListGroup.Item variant="success" action href="#Clinic">Clinic</ListGroup.Item>
                                  <ListGroup.Item variant="success" action href="#Doctors">Doctors</ListGroup.Item>
                                  <ListGroup.Item variant="success" action href="#Contact">Contact</ListGroup.Item>
                                  <ListGroup.Item variant="success" action href="#RegisterForm">Register</ListGroup.Item>
                              </ListGroup>
                              <Button variant="success" className="ms-lg-2">Make an appointment</Button>
                          </Navbar.Collapse>
                      </Container>
                  </Navbar>
                  </header>
              <Ballina />
              <PatientCRUD />
                  <IntervalSlider images={images} interval={3000} />
                  <Card />
                  <Footer />
              </div>
              );
}

class App extends Component {
    render() {
    return( 
        <Suspense fallback={<div>Loading...</div>}>
                <HashRouter>
                <Routes>
                    <Route path="/" element={<Header />}></Route>
                    <Route path="/RegisterForm" element={<RegisterForm />}></Route>
                    <Route path="/LoginForm" element={<LoginForm />}></Route>
                    <Route path="/AboutUs" element={<AboutUs />}></Route>

                    </Routes>
                </HashRouter >
        </Suspense>
    );
        }
}

export default App; 
