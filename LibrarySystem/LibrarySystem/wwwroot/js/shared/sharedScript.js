// Cloudinary upload image setup
//---------------------------------------
var cloudinaryConfig = {
    cloud_name      : 'dbqnukuxd',
    upload_preset   : 'ImageUploadUnSigned',
    folder          : "InstituteLibrary",
    sources         : ['local', 'image_search'],
    google_api_key  : 'AIzaSyAk4tL3B5mTL7zAq9OmRVSV-aozE9cW754',
    search_by_sites : ["all", "cloudinary.com"],
    search_by_rights: true
}
var openCloudinaryWidget = (config, preview, triggered) => {
    try {
        if (!config) {
            console.log("Cloudinary Image Upload Error, config is not available.");
            return "";
        }
        cloudinary.openUploadWidget(config, (error, response) => {
            if (response) {
                // end able the hidden div
                $("." + preview).show();

                // disable the upload control
                $("." + triggered).hide();

                // append the response url to the image source
                $("." + preview + " img").attr("src", response[0].thumbnail_url);

                var successMessage = triggered.indexOf("logo") > -1 ? "Logo" : "Favicon";

                $.notify({
                    icon: "fa fa-check",
                    title: '<strong>' + successMessage + ' Upload! </strong>',
                    message: successMessage + " uploaded success."
                }, {
                        type: 'success',
                        placement: {
                             from: 'top',
                            align: 'right'
                        }
                    });
            }
        });
    } catch (e) {
        $.notify({
            icon: "fa fa-times",
            title: '<strong>System Error : Cloudinary Image Upload Error ! </strong>',
            message: 'Error : ' + e
        }, {
                type: 'success',
                placement: {
                    from: 'top',
                    align: 'left'
                }
            });
        console.log("Cloudinary Image Upload Error : ", e)
    }
}


// Form validation 
var formSerialize =() => {
    try {
        // serializing the form to get the fields names and value
        var form_serialized = $("form").serializeArray();


        if (form_serialized && form_serialized.length > 0) {

            var skeleton_rule   = {};
            var rule_json       = {};
            var message_json    = {};

            $.each(form_serialized, (index, keyValuePair) => {

                // check if element is required or not
                if (keyValuePair) {
                    jsonFormation(keyValuePair, rule_json, message_json);
                } // End of checking if element is required or not
                
            });

            skeleton_rule["rules"] = rule_json;
            skeleton_rule["messages"] = message_json;
            skeleton_rule["submitHandler"] = function (form) { 
                alert('valid form submitted');
                return false; 
            };

            return skeleton_rule;
        }
    } catch (e) {

    }
};

var jsonSerializedConfig = formSerialize();
$("form").validate(jsonSerializedConfig);









// -------------------------- Helper Functions -------------------------------------
function jsonFormation(keyValuePair, rule_json, message_json) {
    try {
        var field_required          = convertionStringToBoolean($("." + keyValuePair.name).attr("data-required")); // converts the string true to boolean true i.e "true" to true
        var internal_rule_json      = {};
        var internal_message_json   = {};


        if (field_required) {
            internal_rule_json['required']      = field_required;
            internal_message_json['required'] = $("." + keyValuePair.name).attr("data-required-message");
        }


        rule_json[keyValuePair.name]     = internal_rule_json;
        message_json[keyValuePair.name]  = internal_message_json;

    } catch (e) {

    }
}

function convertionStringToBoolean(key) {
    switch (key) {
        case "true":
            return true;
            break;
        case "required":
            return true;
            break;
        case "require":
            return true;
            break;
        default : "false";
    }
}