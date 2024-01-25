﻿namespace Core.CrossCuttingConcers.Constants;
public static class ErrorConstants
{
    public const string EmailAlreadyExists = "error.email_already_exists";
    public const string PhoneAlreadyExists = "error.phone_already_exists";
    public const string IdentificationNumberAlreadyExists = "error.identification_number_already_exists";
    public const string CustomerAlreadyExists = "error.customer_already_exists";
    public const string UserNotFound = "error.user_not_found";
    public const string RecordIsNotFound = "error.record_is_not_found";
    public const string UserNameAlreadyTaken = "error.username_already_taken";
    public const string NameAlreadyTaken = "error.name_already_taken";
    public const string KeyAlreadyTaken = "error.key_already_taken";
    public const string RequestedSimpleDoesNotExist = "error.requested_simple_does_not_exist";
    public const string DidntMakeAnyChanges = "error.didnt_make_any_changes";
    public const string RequestedRecordDoesNotExist = "error.requested_record_does_not_exist";
    public const string UserDoesNotExist = "error.user_does_not_exist";
    public const string CustomerNotFound = "error.customer_not_found";
    public const string UsernameOrPasswordIsWrong = "error.username_or_password_is_wrong";
    public const string InternalErrorTryAgain = "error.internal_error_please_try_again";
    public const string UserInformationHasCreated = "error.user_information_has_created";
    public const string RequestImageDoesNotExist = "error.request_image_does_not_exist";
    public const string RecordIsSystem = "error.record_is_system_record_you_can_not_make_changes_to_the_system_records";
    public const string YouHaveNotPermission = "error.you_have_not_permission";
    public const string YouAreNotAuthorized = "error.you_are_not_authorized";
    public const string VerificationCodeAlreadyTaken = "error.there_is_already_an_active_verification_code";
    public const string PasswordsDoesNotMatch = "error.passwords_does_not_match";
    public const string IsNullOrEmpty = "error.is_null_or_empty";
    public const string CompanyIsRequired = "error.company_is_required";
    public const string DepartmentIsRequired = "error.department_is_required";
    public const string TypeOfUseCanNotBeLost = "error.type_of_use_can_not_be_lost";
    public const string CardCodeAlreadyTaken = "error.card_code_already_taken";
    public const string UserCanNotBeNullOrEmptyForTypeOfUseUser = "error.user_can_not_be_null_or_empty_for_type_of_use_user";
    public const string TypeOfUseCanNotBeClose = "error.type_of_use_can_not_be_close";
    public const string FacilityCodeCanNotBeNullForTypeOfCardWiegand26 = "error.facility_code_can_not_be_null_for_type_of_card_wiegand_26";
    public const string PasswordLengthMustBeFourCharacter = "error.password_length_must_be_four_character";
    public const string PasswordMustBeNumber = "error.password_must_be_number";
    public const string UndefinedIntegrationType = "error.undefined_integration_type";
    public const string PleaseGeneratePassword = "error.please_generate_password";
    public const string CustomerCanNotBeEmpty = "error.customer_can_not_be_empty";
    public const string DatabaseConnectionFailed = "error.database_connection_failed";
    public const string SerialNumberAlreadyTaken = "error.serial_number_already_taken";
    public const string RefundPaymentTypeShouldNotBeCreditCard = "error.refund_payment_type_should_not_be_credit_card";
    public const string ServiceNotFound = "error.service_not_found";
    public const string WorkingTypeNotSupported="error.working_type_not_supported.";
    public const string RangeAlreadyExists = "error.range_already_exists";
    public const string UserHasActiveWorkRecord = "error.user_has_active_work_record";
    public const string UserHasWorkRecordsBetweenTheseDates = "error.user_has_work_records_between_these_dates";
    public const string UserHasActivePostalRecord = "error.user_has_active_postal_record";
    public const string UserHasPostalRecordsBetweenTheseDates = "error.user_has_postal_records_between_these_dates";
    public const string SpsDynamicReferanceIdMustBeUnique = "error.sps_dynamic_referance_id_must_be_unique";
    public const string UserHasShiftRecordInDate="error.user_has_shift_record_in_date";
    public const string ClaimNotFound = "error.claim_not_found";
}
