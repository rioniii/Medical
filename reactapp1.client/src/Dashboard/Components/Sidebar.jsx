import React from "react";
import { Nav } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTachometerAlt, faUser, faCalendar, faReceipt, faBriefcaseMedical, faCommentDots } from "@fortawesome/free-solid-svg-icons";
import { NavLink, useNavigate } from 'react-router-dom';



const Sidebar = () => {
    return (
        <div style={{ display: "flex", height: "100vh" }}>
            <div
                style={{
                    width: "250px",
                    backgroundColor: "#f8f9fa",
                    padding: "20px",
                    boxShadow: "2px 0 5px rgba(0,0,0,0.1)",
                }}
            >
                <div className="text-center mb-4">
                    <img
                        src="https://marketplace.canva.com/EAE8K0GX7fY/1/0/1600w/canva-minimalist-hospital-and-medical-health-logo-0zwcZG1ITOE.jpg" // Replace with your logo URL
                        alt="Logo"
                        style={{ width: "80px" }}
                    />
                    <h4>EyeCare</h4>
                    <small>www.medical.ks</small>
                </div>
                <Nav className="flex-column">

                    <Nav.Link href="/dashboard" className="d-flex align-items-center">
                        <FontAwesomeIcon icon={faTachometerAlt} className="me-2" />
                        Dashboard
                    </Nav.Link>


                    <Nav.Link as={NavLink} to="/Pacientet" className="d-flex align-items-center">
                        <FontAwesomeIcon icon={faUser} className="me-2" />
                        Patients
                    </Nav.Link>

                    <Nav.Link href="/appointments" className="d-flex align-items-center">
                        <FontAwesomeIcon icon={faCalendar} className="me-2" />
                        Appointments
                    </Nav.Link>

                    <Nav.Link href="/payments" className="d-flex align-items-center">
                        <FontAwesomeIcon icon={faReceipt} className="me-2" />
                        Payments
                    </Nav.Link>
                    <Nav.Link href="/services" className="d-flex align-items-center">
                        <FontAwesomeIcon icon={faBriefcaseMedical} className="me-2" />
                        Services
                    </Nav.Link>
                    <Nav.Link href="/chats" className="d-flex align-items-center">
                        <FontAwesomeIcon icon={faCommentDots} className="me-2" />
                        Chats
                    </Nav.Link>
                </Nav>
            </div>
        </div>
    );
};

export default Sidebar;
