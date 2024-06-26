﻿import { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Offcanvas from 'react-bootstrap/Offcanvas';
import 'bootstrap/dist/css/bootstrap.min.css';
import CardDashboard from './CardDashboard.jsx';


function OffCanvas() {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <>
            <Button variant="success" onClick={handleShow} className="dashboard-btn" >
                &#8801;
            </Button>

            <Offcanvas show={show} onHide={handleClose} >
                <Offcanvas.Header closeButton style={{ backgroundColor: '#272b2f', color: 'white' }}> 
                </Offcanvas.Header>
                <Offcanvas.Body style={{ backgroundColor: '#272b2f', color: 'white' }} >
                    <CardDashboard/>
                </Offcanvas.Body>
            </Offcanvas>
        </>
    );
}

export default OffCanvas;