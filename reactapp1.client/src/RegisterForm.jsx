import React, { useState } from 'react';
import { MDBBtn, MDBContainer, MDBCard, MDBCardBody, MDBInput, MDBCheckbox } from 'mdb-react-ui-kit';
import { NavLink } from 'react-router-dom';
import contactImage from './assets/R.jpeg';
import Header from './Header';

function RegisterForm() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&.,])[A-Za-z\d@$!%*?&.,]{8,}$/;

    const handleEmailChange = (e) => {
        setEmail(e.target.value);
    };

    const handlePasswordChange = (e) => {
        setPassword(e.target.value);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (!emailRegex.test(email)) {
            alert('Please enter a valid email address.');
            return;
        }
        if (!passwordRegex.test(password)) {
            alert('Please enter a valid password with at least one letter, one digit, one special character, and a minimum length of 8 characters.');
            return;
        }
        // Continue with form submission
    };

    return (
        <>
            <Header />
            <MDBContainer fluid className='d-flex align-items-center justify-content-center bg-image' style={{ height: '100vh', backgroundImage: `url(${contactImage})`, backgroundSize: 'cover', backgroundPosition: 'center', display: "flex", justifyContent: "center", alignItems: "center" }}>
                <div className='mask gradient-custom-3'></div>
                <MDBCard className='m-5' style={{ maxWidth: '600px' }}>
                    <MDBCardBody className='px-5'>
                        <h2 className="text-uppercase text-center mb-5">Create an account</h2>
                        <MDBInput wrapperClass='mb-4' label='Your Email' size='lg' id='form2' type='email' onChange={handleEmailChange} />
                        <MDBInput wrapperClass='mb-4' label='Password' size='lg' id='form3' type='password' onChange={handlePasswordChange} />
                        <MDBInput wrapperClass='mb-4' label='Repeat your password' size='lg' id='form4' type='password' />
                        <div className='d-flex flex-row justify-content-center mb-4'>
                            <MDBCheckbox name='flexCheck' id='flexCheckDefault' label='I agree all statements in Terms of service' />
                        </div>
                        <MDBBtn className='mb-4 w-100 gradient-custom-4' size='lg' style={{ background: 'green' }} onClick={handleSubmit}>Register</MDBBtn>
                        <NavLink to="/LoginForm"><button className="btn btn-dark btn-sm btn-block" style={{ background: 'green' }} >Log in here</button></NavLink>
                    </MDBCardBody>
                </MDBCard>
            </MDBContainer>
        </>
    );
}

export default RegisterForm;
