import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';
function Header() {

    return (
        <header>
            <Navbar bg="dark" data-bs-theme="dark">
      <Container className="nav">
        <Navbar.Brand href="#home">Medical</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto ms-auto">
                            <Nav.Link href="#home">Home</Nav.Link>
                            <Nav.Link href="#link">About</Nav.Link>
                            <Nav.Link href="#services">Services</Nav.Link>
                            <Nav.Link >Blog</Nav.Link>
                            <Nav.Link>Contact</Nav.Link>
           
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
    );
}
export default Header;