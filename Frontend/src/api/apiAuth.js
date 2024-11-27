import axios from 'axios';

const API_BASE_URL = 'https://capstone-project-703387227873.asia-southeast1.run.app/api/Auth';

export const signIn = (userName, password) => {
  return axios.post(`${API_BASE_URL}/sign-in`, {
    userName,
    password,
  }, {
    headers: {
      'accept': '*/*'
    }
  });
};

export const signUp = (userData) => {
  return axios.post(`${API_BASE_URL}/sign-up`, userData, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
};

export const signOut = (data) => {
  return axios.post(`${API_BASE_URL}/sign-out`, data, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
};

export const refreshTokenAPI = (token, refreshToken, userId) => {
  return axios.post(`${API_BASE_URL}/refresh-token`, {
    token,
    refreshToken,
    userId
  }, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
};

export const changePassword = (data) => {
  return axios.post(`${API_BASE_URL}/change-password`, data, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
};

export const resetPassword = (data) => {
  return axios.post(`${API_BASE_URL}/reset-password`, data, {
    headers: {
      'Content-Type': 'application/json'
    }
  });
};
// const axiosInstance = axios.create();

// axiosInstance.interceptors.request.use(async (config) => {
//   const token = await checkAndRefreshToken();
//   config.headers['Authorization'] = `Bearer ${token}`;
//   return config;
// }, (error) => {
//   return Promise.reject(error);
// });
