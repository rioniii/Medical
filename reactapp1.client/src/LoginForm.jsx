import React, { useState } from 'react';
import { MDBBtn, MDBContainer, MDBCard, MDBCardBody, MDBInput, MDBCheckbox } from 'mdb-react-ui-kit';
import { NavLink, useNavigate } from 'react-router-dom';
import contactImage from './assets/R.jpeg';
import Header from './Header';

function LoginForm() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [isValidEmail, setIsValidEmail] = useState(true);
    const [isValidPassword, setIsValidPassword] = useState(true);
    const navigate = useNavigate();

    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&.,])[A-Za-z\d@$!%*?&.,]{8,}$/;

    // LoginForm.jsx
    // ... (other parts of the file)

    // Function to handle login
    const handleLogin = async (email, password) => {
        try {
            // Replace with your actual login logic
            const response = await loginService.authenticate(email, password);
            if (response.isValid) {
                // Proceed with login success logic
            } else {
                throw new Error('Invalid email or password');
            }
        } catch (error) {
            // Handle error (e.g., show error message to the user)
            console.error('Login error:', error);
        }
    };

    // ... (other parts of the file)


  return (
        <MDBContainer fluid className='d-flex align-items-center justify-content-center bg-image' style={{ backgroundImage: `url(${contactImage})`, backgroundSize: 'cover', backgroundPosition: 'center', height: '100vh' }}>
            <div className='mask gradient-custom-3'></div>
            <MDBCard className='m-5' style={{ maxWidth: '400px', backgroundColor: 'rgba(255, 255, 255, 0.8)', borderRadius: '15px' }}>
                <MDBCardBody className='px-5 py-4'>
                    <h2 className="text-uppercase text-center mb-4">Log In</h2>
                    <MDBInput wrapperClass='mb-4' label='Your Email' size='lg' id='form2' type='email' onChange={(e) => setEmail(e.target.value)} value={email} />
                    {!isValidEmail && <div className="text-danger">Invalid email format</div>}
                    <MDBInput wrapperClass='mb-4' label='Password' size='lg' id='form3' type='password' onChange={(e) => setPassword(e.target.value)} value={password} />
                    {!isValidPassword && <div className="text-danger">Password must contain at least one letter, one number, and be at least 8 characters long</div>}
                    <div className='text-center mb-4'> {/* Center the content */}
                        <NavLink to="/forgot-password" className="text-dark">Forgot Password?</NavLink>
                    </div>
                    <MDBBtn className='mb-4 w-100 gradient-custom-4' size='lg' style={{ background: 'green' }} onClick={handleLogin}>Log In</MDBBtn>
                    <p className="text-center mb-0">Don't have an account? <NavLink to="/RegisterForm" className="text-dark">Register here</NavLink></p>
                </MDBCardBody>
            </MDBCard>
        </MDBContainer>
    );
}

export default LoginForm;
