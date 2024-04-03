import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
function Header() {

    return (
        <header>
            <Navbar sticky="top" >
      <Container className="nav">
        <Navbar.Brand href="#home">Medical</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto ms-auto">
                            <Nav.Link href="#home">Home</Nav.Link>
                            <Nav.Link href="#about">About</Nav.Link>
                            <Nav.Link href="#klinika">Klinika</Nav.Link>
                            <Nav.Link href="#doktoret">Doktoret</Nav.Link>
                            <Nav.Link href="#services">Services</Nav.Link>
                            <Nav.Link >Blog</Nav.Link>
                            <Nav.Link>Contact</Nav.Link>
           
                            <Button style={{ marginLeft: '400px' }} variant="outline-info">Make an appointment</Button>{' '}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
        </header>
    );
}
export default Header;