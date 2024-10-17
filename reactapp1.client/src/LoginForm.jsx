import React, { useState } from 'react';
import { MDBBtn, MDBCard, MDBCardBody, MDBInput } from 'mdb-react-ui-kit';
import { NavLink, useNavigate } from 'react-router-dom';
import contactImage from './assets/R.jpeg';
import Header from './Header';

function Register() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');
    const [successMessage, setSuccessMessage] = useState('');

    const navigate = useNavigate();

    const handleEmailChange = (event) => setEmail(event.target.value);
    const handlePasswordChange = (event) => setPassword(event.target.value);

    const handleRegister = async (event) => {
        event.preventDefault();

        const userData = {
            email: email,
            password: password,
        };

        try {
            const response = await fetch('https://localhost:7107/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                const errorText = await response.text();
                console.error('Login failed:', errorText);
                setErrorMessage('Login failed. Please check your email and password.');
                return;
            }

            const data = await response.json();
            console.log('Login successful:', data);
            localStorage.setItem('token', data.token);
            navigate('/');

        } catch (error) {
            console.error('Login error:', error);
            setErrorMessage('An error occurred. Please try again later.');
        }
    };

    return (
        <div className="register-form-container" style={{
            backgroundImage: `url(${contactImage})`,
            backgroundColor: 'rgba(255, 255, 255, 0.5)',
            backgroundSize: 'cover',
            backgroundPosition: 'center',
            minHeight: '100vh',
            display: 'flex',
            flexDirection: 'column'
        }}>
            <Header />
            <div style={{
                flex: 1,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                padding: '10px'
            }}>
                <MDBCard className='p-3' style={{
                    maxWidth: '400px',
                    width: '100%',
                    backgroundColor: 'rgba(255, 255, 255, 0.9)',
                    borderRadius: '10px',
                    boxShadow: '0px 0px 5px rgba(0, 0, 0, 0.1)'
                }}>
                    <MDBCardBody className='px-4 py-3'>
                        <h2 className="text-uppercase text-center mb-2" style={{ fontSize: '1.5rem' }}>Login</h2>
                        <form onSubmit={handleRegister}>
                            <div style={{ position: 'relative', marginBottom: '1rem' }}>
                                <label style={{
                                    position: 'absolute',
                                    left: '12px',
                                    top: '8px',
                                    transition: '0.2s',
                                    fontSize: '0.9rem',
                             
                                }}>Your Email</label>
                                <MDBInput
                                    size='lg'
                                    type='email'
                                    value={email}
                                    onChange={handleEmailChange}
                                    onFocus={() => setEmail(email)} // Optional: Keeps the label transparent on focus
                                    style={{ padding: '0.5rem 12px', fontSize: '0.9rem' }}
                                />
                            </div>
                            <div style={{ position: 'relative', marginBottom: '1rem' }}>
                                <label style={{
                                    position: 'absolute',
                                    left: '12px',
                                    top: '8px',
                                    transition: '0.2s',
                                    fontSize: '0.9rem',
                                   
                                }}>Password</label>
                                <MDBInput
                                    size='lg'
                                    type='password'
                                    value={password}
                                    onChange={handlePasswordChange}
                                    onFocus={() => setPassword(password)} // Optional: Keeps the label transparent on focus
                                    style={{ padding: '0.5rem 12px', fontSize: '0.9rem' }}
                                />
                            </div>

                            {errorMessage && <div className="text-danger mb-2" style={{ fontSize: '0.8rem' }}>{errorMessage}</div>}
                            {successMessage && <div className="text-success mb-2" style={{ fontSize: '0.8rem' }}>{successMessage}</div>}
                            <MDBBtn
                                className='register-btn'
                                size='lg'
                                type='submit'
                                style={{
                                    width: '120px',
                                    height: '40px',
                                    alignSelf: 'center',
                                    fontSize: '0.9rem',
                                    padding: '0'
                                }}
                            >Login</MDBBtn>
                        </form>
                        <NavLink to="/LoginForm" className="text-center d-block mt-2" style={{ color: 'green', fontSize: '0.9rem' }}>
                            Forget Password
                        </NavLink>
                    </MDBCardBody>
                </MDBCard>
            </div>
        </div>
    );
}

export default Register;
