import React, { useState } from 'react';
import { MDBBtn, MDBContainer, MDBCard, MDBCardBody, MDBInput, MDBCheckbox } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg';
import Header from './Header';



function LoginForm() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [isValidEmail, setIsValidEmail] = useState(true);
    const [isValidPassword, setIsValidPassword] = useState(true);

    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&.,])[A-Za-z\d@$!%*?&.,]{8,}$/;


    const handleLogin = () => {
    
        if (!emailRegex.test(email)) {
            setIsValidEmail(false);
            return;
        } else {
            setIsValidEmail(true);
        }


        if (!passwordRegex.test(password)) {
            setIsValidPassword(false);
            return;
        } else {
            setIsValidPassword(true);
        }
         // Dërgimi i të dhënave të përdoruesit në server përmes axios
         //axios.post('localhost:5173/api/login', { email, password })
         //.then(response => {
         //    console.log('Login successful:', response.data);
         //    // Ruajtja e JWT në cookies
         //    const expiration = new Date(new Date().getTime() + 5 * 60 * 60 * 1000);
         //    // Shton 5 orë në kohën aktuale
         //    Cookies.set('jwt', response.data.token, { expires: expiration });
         //    window.location.href = './src/Ballina.jsx';
         //})
         //.catch(error => {
         //    console.error('Login failed:', error);
         //    // Këtu mund të shtoni logjikën për të treguar një mesazh gabimi
         //});

    };

    return (
        <>
            <Header />
            <MDBContainer fluid className='d-flex align-items-center justify-content-center bg-image' style={{ backgroundImage: `url(${contactImage})`, backgroundSize: 'cover', backgroundPosition: 'center', height: '100vh', display: 'flex', alignItems: 'center' }}>
                <div className='mask gradient-custom-3'></div>
                <MDBCard className='m-5' style={{ maxWidth: '600px', backgroundColor: 'rgba(255, 255, 255, 0.7)' }}>

                    <MDBCardBody className='px-5'>
                        <h2 className="text-uppercase text-center mb-5">LogIn</h2>
                        <MDBInput wrapperClass='mb-4' label='Your Email' size='lg' id='form2' type='email' onChange={(e) => setEmail(e.target.value)} value={email} />
                        {!isValidEmail && <div className="text-danger">Invalid email format</div>}
                        <MDBInput wrapperClass='mb-4' label='Password' size='lg' id='form3' type='password' onChange={(e) => setPassword(e.target.value)} value={password} />
                        {!isValidPassword && <div className="text-danger">Password must contain at least one letter, one number, and be at least 8 characters long</div>}
                        <div className='d-flex flex-row justify-content-center mb-4'>
                            <MDBCheckbox name='flexCheck' id='flexCheckDefault' label='Remember me' />
                        </div>
                        <MDBBtn className='mb-4 w-100 gradient-custom-4' size='lg' style={{ background: 'green' }} onClick={handleLogin}>LogIn</MDBBtn>
                        <NavLink to="/RegisterForm"><button className="btn btn-dark btn-sm btn-block" style={{ background: 'green' }}>Register here</button></NavLink>
                    </MDBCardBody>
                </MDBCard>
            </MDBContainer>
        </>
    );
}

export default LoginForm;
