import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';

function CardDashboard() {
    return (
        <Card style={{ width: '18rem' }}>
            <Card.Body>
                <Card.Title>Rion Hoxha</Card.Title>
                <Card.Text>
                    Admin
                </Card.Text>
            </Card.Body>
            <ListGroup className="list-group-flush">
                <ListGroup.Item>Menu</ListGroup.Item>
                <ListGroup.Item>Search</ListGroup.Item>
            </ListGroup>
            <Card.Body>
                <Card.Link href="#">Sign Out</Card.Link>
            </Card.Body>
        </Card>
    );
}

export default CardDashboard;