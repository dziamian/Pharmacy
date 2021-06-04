<template>

<b-container>
    <b-row>   
        <b-col class="col-md-6 mb-5 mb-md-0">
            <h2 class="h3 mb-3 text-black">Billing Details</h2>
            <div class="p-3 p-lg-5 border">

                <div class="form-group">
                    <label for="country" class="text-black">Country <span class="text-danger">*</span></label>
                    <select id="country" class="form-control">
                        <option value="1">Select a country</option>
                        <option value="2">Poland</option>
                        <option value="3">Germany</option>
                        <option value="4">France</option>
                        <option value="5">Spain</option>
                        <option value="6">Russia</option>
                        <option value="7">The Czech Republic</option>
                        <option value="8">Slovakia</option>
                    </select>
                </div>

                <div class="form-group row">
                    <b-col class="md-6">
                        <label for="firstName" class="text-black">First Name <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="firstName" name="firstName">
                    </b-col>
                    <b-col class="md-6">
                        <label for="lastName" class="text-black">Last Name <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="lastName" name="lastName">
                    </b-col>
                </div>
        
                <div class="form-group row">
                    <b-col class="md-12">
                        <label for="companyName" class="text-black">Company Name </label>
                        <input type="text" class="form-control" id="companyName" name="companyName">
                    </b-col>
                </div>
        
                <div class="form-group row">
                    <b-col class="md-12">
                        <label for="address" class="text-black">Address <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="address" name="address" placeholder="Street address">
                    </b-col>
                </div>
        
                <div class="form-group">
                    <input type="text" class="form-control" placeholder="Apartment, suite, unit etc.">
                </div>

                <div class="form-group row">
                    <b-col class="md-6">
                        <label for="provinceCountry" class="text-black">Province / Country <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="provinceCountry" name="provinceCountry">
                    </b-col>
                    <b-col class="md-6">
                            <label for="postcode" class="text-black">Postcode<span class="text-danger">*</span></label>
                            <input type="text" class="form-control" id="postcode" name="postcode">
                    </b-col>
                </div>

                <div class="form-group row mb-5">
                    <b-col class="md-6">
                        <label for="emailAdress" class="text-black">Email Address <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="emailAdress" name="emailAdress">
                    </b-col>
                    <b-col class="md-6">
                        <label for="phoneNumber" class="text-black">Phone <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="phoneNumber" name="phoneNumber" placeholder="Phone Number">
                    </b-col>
                </div>
            </div>
        </b-col>
    </b-row>
</b-container>
</template>

<script>

export default {

}

</script>

<style scoped>

</style>
