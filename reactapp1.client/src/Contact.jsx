import { useState } from 'react';
import { useForm } from 'react-hook-form';
import emailjs from '@emailjs/browser';
import './contact.css';
import Footer from './Footer';
import Header from './Header';
import contactImage from './assets/R.jpeg';
import rightImage from './assets/OIP.jpeg'; // Import the image for the right side

const Contact = () => {
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors }
    } = useForm();
    const [disabled, setDisabled] = useState(false);
    const [alertInfo, setAlertInfo] = useState({
        display: false,
        message: '',
        type: ''
    });

    const toggleAlert = (message, type) => {
        setAlertInfo({ display: true, message, type });

        setTimeout(() => {
            setAlertInfo({ display: false, message: '', type: '' });
        }, 5000);
    };

    const onSubmit = async (data) => {
        const { name, email, subject, message } = data;
        try {
            setDisabled(true);

            const templateParams = {
                name,
                email,
                subject,
                message
            };

            await emailjs.send(
                process.env.REACT_APP_SERVICE_ID,
                process.env.REACT_APP_TEMPLATE_ID,
                templateParams,
                process.env.REACT_APP_USER_ID
            );

            toggleAlert('Form submission was successful!', 'success');
        } catch (e) {
            console.error(e);
            toggleAlert('Uh oh. Something went wrong.', 'danger');
        } finally {
            setDisabled(false);
            reset();
        }
    };

    return (
        <div>
            <Header />
            <div className="contact-container" style={{ backgroundImage: `url(${contactImage})`, backgroundSize: 'cover', backgroundPosition: 'center', width: "100%", display: "flex", justifyContent: "center", alignItems: "center" }}>
                <div className='ContactForm' style={{ backgroundIm: `url(${contactImage})`, backgroundSize: 'cover', backgroundPosition: 'center', width: "100%" , }} >
                    <div className='container' style={{  backgroundColor: "rgba(255, 255, 255, 0.7)" }} >
                        <div className='row'>
                            <div className='col-12 col-md-6 text-center'>
                                <div className='contactForm'>
                                    <form id='contact-form' onSubmit={handleSubmit(onSubmit)} noValidate>
                                        <div className="text-center mb-4" >
                                            <h1>Contact Us</h1>
                                        </div>
                                        <div className='row formRow'>
                                            <div className='col'>
                                                <input
                                                    type='text'
                                                    name='name'
                                                    {...register('name', {
                                                        required: { value: true, message: 'Please enter your name' },
                                                        maxLength: {
                                                            value: 30,
                                                            message: 'Please use 30 characters or less'
                                                        }
                                                    })}
                                                    className='form-control formInput'
                                                    placeholder='Name'
                                                />
                                                {errors.name && <span className='errorMessage'>{errors.name.message}</span>}
                                            </div>
                                        </div>
                                        <div className='row formRow'>
                                            <div className='col'>
                                                <input
                                                    type='email'
                                                    name='email'
                                                    {...register('email', {
                                                        required: true,
                                                        pattern: /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/
                                                    })}
                                                    className='form-control formInput'
                                                    placeholder='Email address'
                                                />
                                                {errors.email && (
                                                    <span className='errorMessage'>Please enter a valid email address</span>
                                                )}
                                            </div>
                                        </div>
                                        <div className='row formRow'>
                                            <div className='col'>
                                                <input
                                                    type='text'
                                                    name='subject'
                                                    {...register('subject', {
                                                        required: { value: true, message: 'Please enter a subject' },
                                                        maxLength: {
                                                            value: 75,
                                                            message: 'Subject cannot exceed 75 characters'
                                                        }
                                                    })}
                                                    className='form-control formInput'
                                                    placeholder='Subject'
                                                />
                                                {errors.subject && (
                                                    <span className='errorMessage'>{errors.subject.message}</span>
                                                )}
                                            </div>
                                        </div>
                                        <div className='row formRow'>
                                            <div className='col'>
                                                <textarea
                                                    rows={3}
                                                    name='message'
                                                    {...register('message', {
                                                        required: true
                                                    })}
                                                    className='form-control formInput'
                                                    placeholder='Message'
                                                />
                                                {errors.message && <span className='errorMessage'>Please enter a message</span>}
                                            </div>
                                        </div>
                                        <div className="text-center mt-4">
                                            <button className='submit-btn' type='submit'>
                                                Submit
                                            </button>
                                        </div>
                                    </form>
                                </div>
                            </div>
                            <div className='col-12 col-md-6'>
                                <img src={rightImage} alt="Right Image" style={{ width: "100%", opacity: 0.8 }} />
                            </div>
                        </div>
                    </div>
                    {alertInfo.display && (
                        <div className={`alert alert-${alertInfo.type} alert-dismissible mt-5`} role='alert'>
                            {alertInfo.message}
                            <button
                                type='button'
                                className='btn-close'
                                data-bs-dismiss='alert'
                                aria-label='Close'
                                onClick={() => setAlertInfo({ display: false, message: '', type: '' })}
                            />
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default Contact;
