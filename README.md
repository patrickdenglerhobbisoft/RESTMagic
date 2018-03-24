#  RESTMagic

RESTMagic makes it very simple to surface REST Api's from your existing data stores.  RESTMagic uses C# reflection and pattern matching to allow easy '1-click' generation of REST services and yet still allows for isolated custom business rule development.


# The Basics

There is really only one REST interface for all of underlying data requests in RESTMagic (though you can surface specific paths at will).  For development purposes, (that is, start thinking from here) your REST will all ultimately map through 
http://{HostName}/api/v1.0/Get/

with a single parameter of type QueryModel

