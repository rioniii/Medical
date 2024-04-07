import React, { Component } from 'react';

import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';

import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import ListGroup from 'react-bootstrap/ListGroup';

import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';

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
                          <Navbar.Brand href="App.jsx">Medical</Navbar.Brand>
                          <Navbar.Toggle aria-controls="basic-navbar-nav" />
                          <Navbar.Collapse id="basic-navbar-nav">
                          

                              <ListGroup horizontal className="NavList">
                                  <ListGroup.Item className="item"  action variant="success"  href="App.jsx">Home</ListGroup.Item>
                                  <ListGroup.Item  variant="success"action href="#About" target="_blank">About</ListGroup.Item>
                                      <ListGroup.Item action variant="success"  href="#Clinic" >Clinic</ListGroup.Item>
                                      <ListGroup.Item variant="success" action href="#Doctors" >Doctors</ListGroup.Item>
                                      <ListGroup.Item variant="success" action href="#Contact" >Contact</ListGroup.Item>
                                      <ListGroup.Item variant="success" action href="#Register" >Register</ListGroup.Item>
                                  </ListGroup>


                              <Button style={{ marginLeft: '290px' }} variant="outline-success">Make an appointment</Button>{' '}
                            
                          </Navbar.Collapse>
                      </Container>
                  </Navbar>
              </header>
              <Ballina/>
              <IntervalSlider images={images} interval={3000}/>
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