import axios from 'axios';
import axiosInstance from './axiosInstance';

const API_BASE_URL = "https://twosport-api-offcial-685025377967.asia-southeast1.run.app/api/CartWithRedis";

export const addToCartAPI = (token, productId, quantity) => {

  return axiosInstance.post(`${API_BASE_URL}/add-to-cart`, {
    productId,
    quantity,
  }, {
    headers: {
      'Accept': '*/*',
      "Authorization": `Bearer ${token}`,
      "Content-Type": "application/json"
    }
  });
};

export const getCartAPI = (token) => {
  const url = `${API_BASE_URL}/get-cart`;
  // const data = {
  //   perPage: 2,
  //   currentPage: 0,
  //   sortBy: sortBy,
  //   isAscending: true
  // };
  return axiosInstance.get(url, {
    headers: {
      'Accept': '*/*',
      "Authorization": `Bearer ${token}`,
      'Content-Type': 'application/json'
    },
    // data: JSON.stringify(data)
  });
};

export const getCartItems = (id) => {
  const url = `${API_BASE_URL}/get-cart-item/${id}`;
  return axios.get(url, {
    headers: {
      'accept': '*/*'
    }
  });
};

export const reduceCartItemAPI = (id, token) => {
  const url = `${API_BASE_URL}/reduce-cart/${id}`;
  return axiosInstance.put(url, {}, {
    headers: {
      'accept': '*/*',
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });
};

export const remmoveCartItemAPI = (cartItemId , token) => {
  const url = `${API_BASE_URL}/delete-cart-item/${cartItemId }`;
  return axiosInstance.delete(url, {
    headers: {
      'accept': '*/*',
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });
};

export const updateCartItemQuantityAPI = (cartItemId, quantity, token) => {
  const url = `${API_BASE_URL}/update-quantity-cart-item/${cartItemId}`;
  return axiosInstance.put(url, { quantity }, {
    headers: {
      'accept': '*/*',
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });
};