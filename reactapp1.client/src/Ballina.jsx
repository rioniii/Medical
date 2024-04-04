import  Button from 'react-bootstrap/Button';
import image1 from './assets/Ballina.png';


function Ballina() {

    return (
    
        <div>
            <img src={image1} style={{ width: '100%', margin: '-1px' }}></img>
            <Button className="service" style={{ margin: '-220px 0px 290px 236px', background: '#234820' }}> Take a Service</Button>
        </div>
    );

}
export default Ballina;