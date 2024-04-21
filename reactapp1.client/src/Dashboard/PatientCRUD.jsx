import React from "react"
import Navigation from "./Components/Navigation.jsx";
import TableDashboard from "./Components/TableDashboard.jsx";
import Button  from "react-bootstrap/Button";


function PatientCRUD(){
    return (
        <>
            <Navigation />
            <br></br>
            <div> 
                <h2 className="dashboard-title">Dashboard</h2>
                <h6 className="dashboard-title" style={{color:'grey' }}>Welcome to Dashboard Admin</h6>
            </div>
            <br></br>
            <div className="AddPatient">
                <Button variant="primary"  >Add Patient</Button>
            </div>
            <br></br>
            <TableDashboard/>
        </>
    
    )
}

export default PatientCRUD;