import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { FiArrowLeft } from 'react-icons/fi';

import api from "../../services/api";

import './style.css';

import logoImage from '../../assets/logo.svg';

export default function NewBook() {
    const navigate = useNavigate();
    
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [launchDate, setLaunchDate] = useState('');
    const [price, setPrice] = useState('');

    async function createBook(e) {
        e.preventDefault();

        const data = {
            title,
            author,
            launchDate,
            price
        };

        const accessToken = localStorage.getItem('accessToken');

        try {
            console.log(data);
            const response = await api.post('api/Book/v1', data, {
                headers: {
                    Authorization: `Bearer ${accessToken}`
                }
            })
            console.log(response);
        } catch (error) {
            alert('Error while recording Book! Try again!');
        }

        navigate('/books');
    }

    return (
        <div className="new-book-container">
            <div className="content">
                <section className="form">
                    <img src={logoImage} alt="DSchaly" />
                    <h1>Add New Book</h1>
                    <p>Enter the book information, then click on Add!</p>
                    <Link className="back-link" to="/books">
                        <FiArrowLeft size={16} color="#251FC5" />
                        Home
                    </Link>
                </section>

                <form onSubmit={createBook}>
                    <input 
                        type="text" 
                        placeholder='Title'
                        value={title}
                        onChange={e => setTitle(e.target.value)}
                    />
                    <input 
                        type="text" 
                        placeholder='Author'
                        value={author}
                        onChange={e => setAuthor(e.target.value)}
                    />
                    <input 
                        type="date"
                        value={launchDate}
                        onChange={e => setLaunchDate(e.target.value)}
                    />

                    <input 
                        type="text" 
                        placeholder='Price'
                        value={price}
                        onChange={e => setPrice(e.target.value)}
                    />

                    <button className="button" type="submit">Add</button>
                </form>
            </div>
        </div>
    );
}