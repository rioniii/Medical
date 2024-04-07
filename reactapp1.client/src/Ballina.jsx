import React from 'react';
import Button from 'react-bootstrap/Button';
import image1 from './assets/Ballina.png';
import 'bootstrap/dist/css/bootstrap.min.css';
import ListGroup from 'react-bootstrap/ListGroup';
import Image from 'react-bootstrap/Image';
function Ballina() {
    const cities = ['Prishtine', 'Gjilan', 'Ferizaj', 'Peje'];
    return (  
        <>
            <Image src={image1} fluid style={{ width: '100%', margin: '-1px' }} ></Image>
            <Button className="service" style={{ margin: '-220px 0px 290px 236px', background: '#234820' }}> Take a Service</Button>
            <ListGroup horizontal>
                {cities.map((item) => <ListGroup.Item action variant="danger"   key={item}>{item}</ListGroup.Item>)}
            </ListGroup>

        </>
    );

}
export default Ballina;