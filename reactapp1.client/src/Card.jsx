import React from 'react'; // Corrected import for React
import examplePicture from './assets/Dr.-Shpetim-Thaci.jpg';
import 'bootstrap/dist/css/bootstrap.min.css';
import Button from 'react-bootstrap/Button'; // Import Button from react-bootstrap
import Alert from 'react-bootstrap/Alert'; // Import Alert from react-bootstrap


function Card() {


    return (
        <div className="card">
            <img className="card-img" src={examplePicture} alt="profile picture"></img>
            <h2> Dr.Shp&euml;tim Thaqi</h2>
            <p>MD. PhD Specialist i Pulmologjise</p>
            <Button>Click me</Button>
            <Alert>This is a button</Alert>
        </div>   
 
    );
}

export default Card;