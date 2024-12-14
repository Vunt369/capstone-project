import React, { useEffect, useState, useRef } from "react";
import { Link, NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faMagnifyingGlass,
  faLocationDot,
  faPhone,
  faEnvelope,
  faCaretDown,
  faUser,
  faCartShopping,
  faBinoculars,
} from "@fortawesome/free-solid-svg-icons";
import GetCurrentLocation from "../components/GetCurrentLocation";
import { useTranslation } from "react-i18next";
import i18n from "i18next";
import { Switch } from "@headlessui/react";
import SignInModal from "../components/Auth/SignInModal";
import { motion, useScroll } from "framer-motion";
import { BreadcrumbsDefault } from "./BreadcrumbsDefault";
import SearchBar from "../components/Product/SearchBar";
import BranchSystem from "../components/BranchButton";
import { getUserCart } from "../services/cartService";
import { useCart } from "../components/Cart/CartContext";
import SearchOrderDropDown from "../components/Research/SearchOrderDropDown";

function Header() {
  const { scrollYProgress } = useScroll();
  const { t } = useTranslation("translation");
  const [enabled, setEnabled] = useState(true);
  const { cartCount, setCartCount } = useCart();
  const token = localStorage.getItem("token");

  const [prevScrollY, setPrevScrollY] = useState(0);
  const [visible, setVisible] = useState(true);

  const [isPolicyDropdownOpen, setIsPolicyDropdownOpen] = useState(false);
  const dropdownRef = useRef(null);

  useEffect(() => {
    const handleClickOutside = (event) => {
      if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
        setIsPolicyDropdownOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const handleLinkClick = () => {
    setIsPolicyDropdownOpen(false);
  };

  useEffect(() => {
    const handleScroll = () => {
      if (window.scrollY > prevScrollY) {
        // Scrolling down, hide the header
        setVisible(false);
      } else {
        // Scrolling up, show the header
        setVisible(true);
      }
      setPrevScrollY(window.scrollY);
    };

    window.addEventListener("scroll", handleScroll);
    return () => {
      window.removeEventListener("scroll", handleScroll);
    };
  }, [prevScrollY]);

  useEffect(() => {
    const fetchCartCount = async () => {
      const token = localStorage.getItem("token");
      if (token) {
        const cartData = await getUserCart(token);
        const totalItems = cartData.reduce(
          (acc, item) => acc + item.quantity,
          0
        );
        setCartCount(totalItems);
      }
    };

    fetchCartCount();
  }, [setCartCount]);

  // useEffect(() => {
  //   const fetchCart = async () => {

  //     if (token) {
  //       const cartData = await getUserCart(token);
  //       const count = cartData.reduce((total, item) => total + item.quantity, 0);
  //       setCartCount(count);
  //     }
  //   };

  //   fetchCart();
  // }, []);

  const changeLanguage = () => {
    const languageValue = enabled ? "eng" : "vie";
    i18n.changeLanguage(languageValue);
  };
  return (
    <>
      <div className="w-full relative z-50 pb-28">
        <div
          className={`fixed top-0 left-0 right-0 transition-all duration-300 ease-in-out ${
            visible ? "transform translate-y-0" : "transform -translate-y-full"
          }`}
        >
          {" "}
          <div className="bg-white/95 backdrop-blur-lg font-medium text-black flex justify-between items-center relative text-xs py-2 z-50">
            <div className="flex pl-20 items-center space-x-2">
              <Link to="/">
                <img
                  src="/assets/images/Logo.png"
                  alt="2Sport"
                  className="max-w-sm max-h-8 pr-3"
                />
              </Link>
              <FontAwesomeIcon icon={faLocationDot} />
              {/* <p>Ho Chi Minh, Viet Nam</p> */}
              <GetCurrentLocation />
              <div className=" pl-5">
                <BranchSystem />
              </div>

              {/* <Switch
                                checked={enabled}
                                onChange={() => {
                                    setEnabled(!enabled);
                                    changeLanguage();
                                }}
                                className={`${enabled ? 'bg-orange-200' : 'bg-orange-500'
                                    }  relative inline-flex h-5 w-10 shrink-0 cursor-pointer rounded-full border-2 border-transparent transition-colors duration-200 ease-in-out focus:outline-none focus-visible:ring-2  focus-visible:ring-white/75`}
                            >
                                <span
                                    aria-hidden="true"
                                    className={`${enabled ? 'translate-x-0' : 'translate-x-5'
                                        } pointer-events-none inline-block h-4 w-4 transform rounded-full bg-white shadow-lg ring-0 transition duration-200 ease-in-out`}
                                />
                            </Switch>
                            <span className="text-orange-500">{enabled ? 'VI' : 'EN'}</span> */}
            </div>
            {/*search*/}
            <SearchBar />

            {/* <div className="flex w-1/4 bg-white border-2 border-orange-500 rounded-full  p-2 mx-auto">
                            <input
                                className="flex-grow bg-transparent outline-none placeholder-gray-400"
                                placeholder="Enter your search keywords here"
                                type="text"
                            />
                            <FontAwesomeIcon icon={faMagnifyingGlass} className="items-center text-orange-500 font-medium pr-3" />
                        </div> */}
            <div className="flex pr-20 items-center space-x-4">
              <p>
                <FontAwesomeIcon icon={faPhone} className="pr-1" />
                +84 338-581-571
              </p>
              <p>
                <FontAwesomeIcon icon={faEnvelope} className="pr-1" />
                2sportteam@gmail.com
              </p>
              {/* <select onChange={changeLanguage} className="text-orange-500">
                                <option value="eng">English</option>
                                <option value="vie">Vietnamese</option>
                            </select> */}
            </div>
          </div>
          <div className="bg-zinc-800/80 backdrop-blur-lg text-white  flex justify-between items-center text-base font-normal py-5 pr-20  z-50">
            <div className="flex space-x-10 pl-20 ">
              <Link
                to="/"
                className=" hover:text-orange-500 focus:text-orange-500"
              >
                {t("header.home")}
              </Link>
              <Link
                to="/product"
                className=" hover:text-orange-500 focus:text-orange-500"
              >
                {t("header.product")}
                {/* <FontAwesomeIcon icon={faCaretDown} className="pl-2" /> */}
              </Link>
              {/* <Link to="/">{t("header.blog")}</Link> */}
              <Link
                to="/about-us"
                className=" hover:text-orange-500 focus:text-orange-500"
              >
                {t("header.about")}
              </Link>
              <Link
                to="/contact-us"
                className=" hover:text-orange-500 focus:text-orange-500"
              >
                {t("header.contact")}
              </Link>
              <Link
                to="/blog"
                className=" hover:text-orange-500 focus:text-orange-500"
              >
                Blog
              </Link>
              <Link
                to="/manage-account/refund-request"
                className=" hover:text-orange-500 focus:text-orange-500"
              >
                Trả hàng/Hoàn tiền
              </Link>
              <div
                className="relative"
                ref={dropdownRef}
                onMouseEnter={() => setIsPolicyDropdownOpen(true)} 
                onMouseLeave={() => setIsPolicyDropdownOpen(false)} 
              >
                <button className="hover:text-orange-500 transition-colors duration-200">
                  Chính sách
                </button>
                {isPolicyDropdownOpen && (
                  <div className="absolute left-0 w-80 bg-white text-black shadow-lg rounded-md py-2 z-50">
                    <Link
                      to="/complaints-handling"
                      className="block px-4 py-2 hover:bg-gray-100 transition-colors duration-200"
                    >
                      Chính sách xử lý khiếu nại
                    </Link>
                    <Link
                      to="/returns-refunds"
                      className="block px-4 py-2 hover:bg-gray-100 transition-colors duration-200"
                    >
                      Chính sách đổi trả, hoàn tiền
                    </Link>
                    <Link
                      to="/payment"
                      className="block px-4 py-2 hover:bg-gray-100 transition-colors duration-200"
                    >
                      Chính sách thanh toán
                    </Link>
                    <Link
                      to="/privacy"
                      className="block px-4 py-2 hover:bg-gray-100 transition-colors duration-200"
                    >
                      Chính sách bảo mật thông tin khách hàng
                    </Link>
                    <Link
                      to="/membership"
                      className="block px-4 py-2 hover:bg-gray-100 transition-colors duration-200"
                    >
                      Chính sách dành cho membership khi thuê đồ tại 2Sport
                    </Link>
                    <Link
                      to="/second-hand-rentals"
                      className="block px-4 py-2 hover:bg-gray-100 transition-colors duration-200"
                    >
                      Chính sách khi thuê đồ 2hand tại 2Sport
                    </Link>
                    <Link
                      to="/shipping"
                      className="block px-4 py-2 hover:bg-gray-100 transition-colors duration-200"
                    >
                      Chính sách vận chuyển
                    </Link>
                  </div>
                )}
              </div>
            </div>
            <div className="flex space-x-4">
              <SearchOrderDropDown />
              <SignInModal />
              <Link to="/cart" className="flex space-x-2">
                <div className="relative">
                  <FontAwesomeIcon icon={faCartShopping} className="pr-1" />
                  {cartCount > 0 && token && (
                    <span className="absolute -top-2 -right-2 bg-red-500 text-white text-[0.625rem] font-bold rounded-full h-[1rem] w-4  leading-none flex items-center justify-center">
                      {cartCount}
                    </span>
                  )}
                </div>
                <p>Giỏ hàng</p>
              </Link>
            </div>
          </div>
          <motion.div
            className="progress-bar"
            style={{ scaleX: scrollYProgress, zIndex: "-99999" }}
          />
          {/* <BreadcrumbsDefault/> */}
        </div>
      </div>
    </>
  );
}

export default Header;
