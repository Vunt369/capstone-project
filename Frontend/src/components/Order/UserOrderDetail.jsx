import React, { useState, useEffect } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import axios from "axios";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faArrowLeft,
  faUser,
  faEnvelope,
  faPhone,
  faMapMarkerAlt,
  faShoppingCart,
  faMoneyBillWave,
  faCalendarAlt,
  faTruck,
  faBolt,
  faDollarSign,
  faVenusMars,
  faClock,
  faCheckCircle,
  faCogs,
  faFlagCheckered,
  faArrowsDownToLine,
} from "@fortawesome/free-solid-svg-icons";
import {
  Button,
  Step,
  Stepper,
  Tooltip,
  Typography,
} from "@material-tailwind/react";
import { submitReview } from "../../services/reviewService";
import StarRating from "../Product/StarRating";
import { toast } from "react-toastify";
import DoneSaleOrderButton from "../User/DoneSaleOrderButton";
import CancelSaleOrderButton from "../User/CancelSaleOrderButton";

export default function UserOrderDetail() {
  const { orderCode } = useParams();
  const navigate = useNavigate();
  const [orderDetail, setOrderDetail] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [reason, setReason] = useState("");
  const [loading, setLoading] = useState(false);
  const [showReviewModal, setShowReviewModal] = useState(false);
  const [reviewData, setReviewData] = useState({ star: 5, review: "" });
  const [currentProduct, setCurrentProduct] = useState(null);
  const [confirmReload, setConfirmReload] = useState(false);
  const [reload, setReload] = useState(false);

  const statusColors = {
    "Chờ xử lý": "bg-yellow-100 text-yellow-800",
    "Đã xác nhận": "bg-orange-100 text-orange-800",
    "Đang xử lý": "bg-purple-100 text-purple-800",
    "Đã giao hàng": "bg-indigo-100 text-indigo-800",
    "Đã giao cho ĐVVC": "bg-blue-100 text-blue-800",
    "Đã hủy": "bg-red-200 text-red-900",
    "Đã hoàn thành": "bg-green-100 text-green-800",
    "Đã thanh toán": "bg-green-100 text-green-800",
  };

  const paymentStatusColors = {
    "Đang chờ thanh toán": "text-yellow-800",
    "Đã đặt cọc": "text-blue-800",
    "Đã thanh toán": "text-green-800",
    "Đã hủy": "text-red-800",
  };

  const ORDER_STEPS = [
    { id: 1, label: "Chờ Xác Nhận Đơn Hàng" },
    { id: 2, label: "Đã Xác Nhận Thông Tin" },
    { id: 3, label: "Shop Đang Xử Lý Đơn Hàng" },
    { id: 4, label: "Đã Giao Cho ĐVVC" },
    { id: 5, label: "Đã Nhận Được Hàng" },
    { id: 11, label: "Đã Hoàn Thành" },
  ];

  const getCurrentStepIndex = (orderStatus) => {
    const step = ORDER_STEPS.find((step) => step.id === orderStatus);
    return step ? ORDER_STEPS.indexOf(step) : -1;
  };

  const fetchOrderDetail = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axios.get(
        `https://capstone-project-703387227873.asia-southeast1.run.app/api/SaleOrder/get-order-by-code?orderCode=${orderCode}`,
        {
          headers: {
            accept: "*/*",
            Authorization: `Bearer ${token}`,
          },
        }
      );

      if (response.data.isSuccess) {
        setOrderDetail(response.data.data);
      } else {
        setError("Failed to fetch order details");
      }
    } catch (err) {
      setError(err.message || "An error occurred while fetching order details");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchOrderDetail();
    getCurrentStepIndex();
  }, [orderCode, reload, confirmReload]);


  if (isLoading)
    return (
      <div className="flex justify-center items-center h-screen">
        <div className="animate-spin rounded-full h-32 w-32 border-t-2 border-b-2 border-blue-500"></div>
      </div>
    );
  if (error)
    return <div className="text-center text-red-500 mt-4">Error: {error}</div>;

  const {
    id,
    fullName,
    email,
    contactPhone,
    address,
    saleOrderCode,
    paymentStatus,
    orderStatus,
    orderStatusId,
    deliveryMethod,
    saleOrderDetailVMs,
    updatedAt,
    paymentDate,
    totalAmount,
  } = orderDetail;

  const products = orderDetail?.saleOrderDetailVMs?.$values || [];

  // const handleDoneOrder = async () => {
  //   if (!orderDetail || !orderDetail.id) {
  //     alert("Không tìm thấy thông tin đơn hàng để hoàn tất.");
  //     return;
  //   }

  //   const confirmDone = window.confirm(
  //     "Bạn có chắc chắn muốn hoàn tất đơn hàng này không?"
  //   );

  //   if (!confirmDone) {
  //     return;
  //   }

  //   const newStatus = 5;

  //   try {
  //     const response = await axios.put(
  //       `https://capstone-project-703387227873.asia-southeast1.run.app/api/SaleOrder/update-order-status/${orderDetail.id}?status=${newStatus}`,
  //       {},
  //       {
  //         headers: {
  //           accept: "*/*",
  //         },
  //       }
  //     );

  //     if (response && response.data.isSuccess) {
  //       alert("Đơn hàng của bạn đã được hoàn tất thành công.");
  //       // setShowReviewModal(true);
  //       fetchOrderDetail();
  //     } else {
  //       alert("Không thể hoàn tất đơn hàng. Vui lòng thử lại sau.");
  //     }
  //   } catch (error) {
  //     console.error("Error updating order status:", error);
  //     alert("Đã xảy ra lỗi khi hoàn tất đơn hàng. Vui lòng thử lại sau.");
  //   }
  // };

  const handleSubmitReview = async () => {
    if (!currentProduct) return;

    try {
      await submitReview(currentProduct.productCode, {
        star: reviewData.star,
        review: reviewData.review,
        status: true,
      });
      alert("Cảm ơn bạn đã đánh giá sản phẩm!");
      setShowReviewModal(false);
    } catch (error) {
      console.error("Error submitting review:", error);
      alert("Gửi đánh giá thất bại. Vui lòng thử lại.");
    }
  };

  return (
    <div className="container mx-auto p-4 min-h-screen bg-gray-200">
      <div className="max-w-4xl mx-auto">
        <div className="flex justify-between items-center mb-6">
          <button
            onClick={() => navigate(-1)}
            className="flex items-center gap-2 text-blue-500 hover:text-blue-700"
          >
            <FontAwesomeIcon icon={faArrowLeft} />
            Quay lại
          </button>
        </div>
        {orderStatus === "Đã hủy" && (
          <div className="bg-yellow-50 p-4 rounded-lg shadow-sm mb-6">
            <p className="text-xl">
              <b className="text-red-500">Đã hủy đơn hàng </b>
              <i className="text-lg">
                vào lúc{" "}
                {new Date(updatedAt).toLocaleString("vi-VN", {
                  day: "2-digit",
                  month: "2-digit",
                  year: "numeric",
                  hour: "2-digit",
                  minute: "2-digit",
                  second: "2-digit",
                })}
              </i>
            </p>
            <p className="flex items-center gap-2 mb-2">
              <FontAwesomeIcon icon={faBolt} style={{ color: "#fd7272" }} />
              <span className="font-semibold">Lý do:</span> {orderDetail.reason}
            </p>
          </div>
        )}
        {paymentStatus === "Đã thanh toán" && (
          <div className="bg-green-500 p-4 rounded-lg shadow-sm mb-6">
            <p className="text-xl">
              <b className="text-white">Đơn hàng đã được thanh toán </b>
              <i className="text-lg">
                vào lúc{" "}
                {new Date(paymentDate).toLocaleString("vi-VN", {
                  day: "2-digit",
                  month: "2-digit",
                  year: "numeric",
                  hour: "2-digit",
                  minute: "2-digit",
                  second: "2-digit",
                })}{" "}
                - {orderDetail.paymentMethod}
              </i>
            </p>
            <p className="flex items-center gap-2 mb-2 text-white">
              <FontAwesomeIcon
                icon={faDollarSign}
                style={{ color: "#FFD43B" }}
              />
              <span className="font-semibold">Số tiền: </span>
              {totalAmount.toLocaleString("Vi-vn")}₫
            </p>
          </div>
        )}
        <div className="bg-white p-6 rounded-lg shadow-lg mb-6">
          <div className="py-4 bg-white rounded-md shadow-sm">
            {/* Stepper */}
            <div className="mb-16 bg-orange-100">
              {console.log(orderDetail)}
              <Stepper
                activeStep={getCurrentStepIndex(orderStatusId)}
                className=" p-2 rounded-lg"
              >
                {ORDER_STEPS.map((status, index) => (
                  <Step
                    key={index}
                    completed={index < getCurrentStepIndex(orderStatusId)}
                    className={`${
                      index < getCurrentStepIndex(orderStatusId)
                        ? "bg-blue-500 text-wrap w-10 text-green-600"
                        : "bg-green-600 text-green-600"
                    }`}
                  >
                    <div className="relative flex flex-col items-center">
                      <div
                        className={`w-10 h-10 flex items-center justify-center rounded-full ${
                          index <= getCurrentStepIndex(orderStatusId)
                            ? "bg-green-500 text-white"
                            : "bg-gray-300 text-gray-600"
                        }`}
                      >
                        <FontAwesomeIcon
                          icon={
                            index === 0
                              ? faClock
                              : index === 1
                              ? faCheckCircle
                              : index === 2
                              ? faCogs
                              : index === 3
                              ? faTruck
                              : index === 4
                              ? faArrowsDownToLine
                              : index === 11
                              ? faFlagCheckered
                              : faClock
                          }
                          className="text-lg"
                        />
                      </div>
                      <div
                        className={`absolute top-12 text-xs font-medium text-wrap w-20 text-center ${
                          index <= getCurrentStepIndex(orderStatusId)
                            ? "text-green-600"
                            : "text-gray-600"
                        }`}
                      >
                        {status.label}
                      </div>
                    </div>
                  </Step>
                ))}
              </Stepper>
            </div>
            <div className="flex justify-between items-center">
              <h2 className="text-2xl font-bold text-gray-800 flex-1">
                MÃ ĐƠN HÀNG MUA-{" "}
                <span className="text-orange-500">#{saleOrderCode}</span>
              </h2>
              <div className="flex items-center mr-10">
                <h2 className="text-2xl font-semibold  text-gray-800 flex-1">
                  {orderStatus.toUpperCase()}
                </h2>
              </div>
            </div>
          </div>
          <hr className="mb-5" />
          <div className="grid grid-cols-4 gap-6">
            <div className="col-span-3">
              <h2 className="text-lg font-bold mb-2 text-gray-700">
                Thông tin khách hàng
              </h2>
              <p className="flex items-center gap-2 mb-2">
                <FontAwesomeIcon icon={faUser} className="text-blue-500" />
                <span className="font-base">Tên:</span> <i>{fullName}</i>
              </p>
              <p className="flex items-center gap-2 mb-2">
                <FontAwesomeIcon
                  icon={faVenusMars}
                  className="text-blue-500 fa-xs"
                />
                <span className="font-base">Giới tính:</span>
                <i>{orderDetail.gender}</i>
              </p>
              <p className="flex items-center gap-2 mb-2">
                <FontAwesomeIcon icon={faEnvelope} className="text-blue-500" />
                <span className="font-base">Email:</span> <i>{email}</i>
              </p>
              <p className="flex items-center gap-2 mb-2">
                <FontAwesomeIcon icon={faPhone} className="text-blue-500" />
                <span className="font-base">Số điện thoại:</span>
                <i> {contactPhone}</i>
              </p>
              <p className="flex items-start gap-2 mb-2">
                <FontAwesomeIcon
                  icon={faMapMarkerAlt}
                  className="text-blue-500"
                />
                <span className="font-base flex-shrink-0">Địa chỉ:</span>
                <span className="break-words">
                  <i>{address}</i>
                </span>
              </p>
              <div className="pt-2">
                <h2 className="text-lg font-bold mb-2 text-gray-700">
                  Thông tin đơn hàng
                </h2>
                <p className="flex items-start gap-2 mb-2 w-full">
                  <FontAwesomeIcon icon={faTruck} className="text-blue-500" />
                  <span className="font-base flex-shrink-0">
                    Phương thức nhận hàng:
                  </span>
                  <span className="break-words">
                    <i>{deliveryMethod}</i>
                  </span>
                </p>
                <p className="flex items-center gap-2 mb-2">
                  <FontAwesomeIcon
                    icon={faCalendarAlt}
                    className="text-blue-500"
                  />
                  <span className="font-base">Tình trạng đơn hàng:</span>{" "}
                  <span
                    className={`px-4 py-2 mr-5 rounded-full text-xs font-bold ${
                      statusColors[orderStatus] || "bg-gray-100 text-gray-800"
                    }`}
                  >
                    {orderStatus}
                  </span>
                </p>
                <p className="flex items-center gap-2 mb-2">
                  <FontAwesomeIcon
                    icon={faMoneyBillWave}
                    className="text-blue-500"
                  />
                  <span className="font-base">Tình trạng thanh toán:</span>{" "}
                  <span
                    className={`py-2 px-4 mr-1.5 rounded-full text-xs font-bold ${
                      statusColors[paymentStatus] || "bg-gray-100 text-gray-800"
                    }`}
                  >
                    {paymentStatus}
                  </span>
                </p>
              </div>
            </div>

            <div className="col-span-1 flex flex-col gap-4">
              {orderStatus === "Chờ xử lý" && paymentStatus === "N/A" && (
                <Button
                  size="sm"
                  className="w-full text-blue-700 bg-white border border-blue-700 rounded-md hover:bg-blue-200"
                  onClick={() =>
                    navigate("/checkout", {
                      state: { selectedOrder: orderDetail },
                    })
                  }
                >
                  Thanh toán
                </Button>
              )}
              {orderDetail.orderStatus === "Chờ xử lý" && (
                <CancelSaleOrderButton
                  saleOrderId={id}
                  setReload={setReload}
                  className="w-full"
                />
              )}
              {orderStatus === "Đã giao cho ĐVVC" && (
                <DoneSaleOrderButton
                  saleOrderId={id}
                  setConfirmReload={setConfirmReload}
                  className="w-full"
                />
              )}
              {/* {REVIEW BUTTON} */}
            </div>
          </div>
        </div>

        <div className="bg-gray-50 p-4 rounded-lg shadow-sm">
          {products.map((product) => (
            <div
              key={product.productId}
              className="bg-gray-50 p-4 mb-4 rounded-lg shadow-sm"
            >
              <div className="flex flex-col md:flex-row gap-4">
                <img
                  src={product.imgAvatarPath}
                  alt={product.productName}
                  className="w-full md:w-32 h-32 object-cover rounded"
                />
                <div className="flex-grow">
                  <h4 className="font-semibold text-lg mb-2 text-orange-500">
                    <Link to={`/product/${product.productCode}`}>
                      {product.productName}
                    </Link>
                  </h4>
                  <div className="grid grid-cols-2 gap-2">
                    <p>
                      <span className="font-semibold">Màu sắc:</span>{" "}
                      <i>{product.color}</i>
                    </p>
                    <p>
                      <span className="font-semibold">Số lượng:</span>{" "}
                      <i>{product.quantity}</i>
                    </p>
                    <p>
                      <span className="font-semibold">Kích thước:</span>{" "}
                      <i>{product.size}</i>
                    </p>
                    <p>
                      <span className="font-semibold">Đơn giá bán:</span>{" "}
                      <i>{product.unitPrice.toLocaleString("Vi-VN")}₫</i>
                    </p>
                    <p>
                      <span className="font-semibold">Tình trạng:</span>{" "}
                      <i>{product.condition}%</i>
                    </p>
                    <p className="flex items-center">
                      <span className="flex items-center gap-1">
                        <span className="font-semibold">Tổng tiền:</span>
                        <i className="ml-2">
                          {product.totalAmount.toLocaleString("vi-VN")}₫
                        </i>
                        <Tooltip
                          content={
                            <div className="w-80">
                              <Typography color="white" className="font-medium">
                                Tổng tiền:
                              </Typography>
                              <Typography
                                variant="small"
                                color="white"
                                className="font-normal opacity-80"
                              >
                                Tổng tiền được tính: Số lượng x Đơn giá bán
                              </Typography>
                            </div>
                          }
                        >
                          <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            stroke="currentColor"
                            strokeWidth={2}
                            className="h-4 w-4 cursor-pointer text-blue-gray-500"
                          >
                            <path
                              strokeLinecap="round"
                              strokeLinejoin="round"
                              d="M11.25 11.25l.041-.02a.75.75 0 011.063.852l-.708 2.836a.75.75 0 001.063.853l.041-.021M21 12a9 9 0 11-18 0 9 9 0 0118 0zm-9-3.75h.008v.008H12V8.25z"
                            />
                          </svg>
                        </Tooltip>
                      </span>
                    </p>
                  </div>
                </div>
              </div>
            </div>
          ))}
        </div>
        <div className="bg-white p-6 rounded-lg shadow-lg mt-6 text-gray-700">
          <p className="text-xl flex justify-between">
            <b>Tạm tính: </b>
            <i>{orderDetail.subTotal.toLocaleString("vi-VN")}₫</i>
          </p>
          <p className="flex justify-between">
            <b className="text-xl py-2 ">Phí vận chuyển: </b>
            <p className="text-sm py-2 ">
              {orderDetail.transportFee || "2Sport sẽ liên hệ và thông báo sau"}
            </p>
          </p>
          <p className="text-xl flex justify-between items-center text-gray-700">
            <span className="flex items-center gap-1">
              <b>Thành tiền:</b>
            </span>
            <i className="text-orange-500 font-bold">
              {orderDetail.totalAmount.toLocaleString("vi-VN")}₫
            </i>
          </p>
        </div>
      </div>
      {/* Review Modal */}
      {showReviewModal && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
          <div className="bg-white p-6 rounded-lg shadow-xl w-96 max-w-full">
            <h3 className="text-2xl font-semibold mb-4 text-gray-800">
              Đánh giá sản phẩm
            </h3>
            <div className="mb-4">
              <label className="block text-sm font-medium text-gray-700 mb-2">
                Đánh giá của bạn
              </label>
              <StarRating
                rating={reviewData.star}
                onRatingChange={(newRating) =>
                  setReviewData({ ...reviewData, star: newRating })
                }
              />
            </div>
            <div className="mb-4">
              <label
                htmlFor="review"
                className="block text-sm font-medium text-gray-700 mb-2"
              >
                Nhận xét
              </label>
              <textarea
                id="review"
                rows="4"
                className="w-full px-3 py-2 text-gray-700 border rounded-lg focus:outline-none focus:border-blue-500"
                value={reviewData.review}
                onChange={(e) =>
                  setReviewData({ ...reviewData, review: e.target.value })
                }
                placeholder="Chia sẻ trải nghiệm của bạn về sản phẩm này..."
              ></textarea>
            </div>
            <div className="flex justify-end space-x-2">
              <Button
                color="gray"
                onClick={() => setShowReviewModal(false)}
                className="px-4 py-2 rounded-lg"
              >
                Hủy
              </Button>
              <button onClick={handleSubmitReview}>
                {loading ? "Đang tiến hành..." : "Gửi đánh giá"}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
