import React, { Component, Suspense } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import Footer from "./Footer";
import Card from "./Card";
import IntervalSlider from './IntervalSlider';
import Ballina from './Ballina';
import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import Button from 'react-bootstrap/Button';
import ListGroup from 'react-bootstrap/ListGroup';
import { Route, Routes, HashRouter } from 'react-router-dom';
import i4 from './assets/i4.jpg';
import i2 from './assets/i2.jpg';
import i3 from './assets/i3.jpg';
import logo from 'C:\Users\W11\source\repos\Medical\reactapp1.client\src\assets\medical care logo template social media .png';


const RegisterForm = React.lazy(() => import("./RegisterForm"));
const LoginForm = React.lazy(() => import("./LoginForm"));
const Header = () => {
const images = [i3, i2, i4];



      return (
          <div>
              <header>
                  <Navbar bg="dark" data-bs-theme="dark">
                      <Container className="nav">
                          <Navbar.Brand href="App.jsx">
                              <img
                                  src={logo}
                                  alt="Your Logo"
                                  height="30"
                                  className="d-inline-block align-top"
                              />
                          </Navbar.Brand>
                          <Navbar.Brand href="App.jsx">Medical</Navbar.Brand>
                          <Navbar.Toggle aria-controls="basic-navbar-nav" />
                          <Navbar.Collapse id="basic-navbar-nav">
                          

                              <ListGroup horizontal className="NavList">
                                  <ListGroup.Item className="item"  action variant="success"  href="App.jsx">Home</ListGroup.Item>
                                  <ListGroup.Item  variant="success"action href="#About" target="_blank">About</ListGroup.Item>
                                      <ListGroup.Item action variant="success"  href="#Clinic" >Clinic</ListGroup.Item>
                                      <ListGroup.Item variant="success" action href="#Doctors" >Doctors</ListGroup.Item>
                                      <ListGroup.Item variant="success" action href="#Contact" >Contact</ListGroup.Item>
                                      <ListGroup.Item variant="success" action href="#RegisterForm" >Register</ListGroup.Item>
                                  </ListGroup>


                              <Button variant="success"   style={{ marginLeft: '290px'}} >Make an appointment</Button>{' '}
                            
                          </Navbar.Collapse>
                      </Container>
                  </Navbar>
              </header>
              <Ballina/>
              <IntervalSlider images={images} interval={3000}/>
              <Card />
              <Footer/>
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
                    </Routes>
                </HashRouter >
        </Suspense>
    );
        }
}

export default App; 