import Container from 'react-bootstrap/Container';
import Navbar from 'react-bootstrap/Navbar';
import OffCanvas from './OffCanvas.jsx'
import 'bootstrap/dist/css/bootstrap.min.css';


function Navigation() {
    return (
        <Navbar bg="dark" data-bs-theme="dark" className="bg-body-tertiary" >
            <Container>
                <Navbar.Brand >Medical</Navbar.Brand>
                <Navbar.Brand ><OffCanvas/></Navbar.Brand>
                    <Navbar.Toggle/>
                <Navbar.Collapse className="justify-content-end">
                    <Navbar.Text>
                        Signed in as: <a>Rion</a>
                        
                    </Navbar.Text>
                </Navbar.Collapse>
            </Container>
        </Navbar>
        
    );
}

export default Navigation;