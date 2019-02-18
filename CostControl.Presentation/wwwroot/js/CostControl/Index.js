$(function () {
    $("#board").empty();
});

$('#Role').click(function () {
    $("#board").load("/Role/RoleList");
});

$('#JobPosition').click(function () {
    $("#board").load("/JobPosition/JobPositionList");
});

$('#JobSection').click(function () {
    $("#board").load("/JobSection/JobSectionList");
});

$('#OrganizationType').click(function () {
    $("#board").load("/OrganizationType/OrganizationTypeList");
});

$('#Organization').click(function () {
    $("#board").load("/Organization/OrganizationList");
});

$('#Person').click(function () {
    $("#board").load("/Person/PersonList");
});

$('#ApiList').click(function () {
    $("#board").load("http://localhost:4130/Help");
});